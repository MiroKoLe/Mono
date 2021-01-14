using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MVC;
using PagedList;
using Project.Model;
using Project.Model.Common;
using Project.Service.Common;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        private Products db = new Products();

        private readonly IProductService service; 

            public ProductController (IProductService _service)
        {
            this.service = _service; 
        }

        // GET: Product
        public async Task<ActionResult> Index(string searchBy, string search, int? page, string sortBy, int? PageSize, int? PageNumber)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortModelParameter = sortBy == "Model" ? "Model desc" : "Model";

            var productList = await service.GetProductsAsync(searchBy, search, sortBy, PageSize, page, PageNumber);
            IList<ProductModel> models = new List<ProductModel>();


            models = Mapper.Map<List<IProduct>, IList<ProductModel>>(productList);

            int pageSize = 5;
            int pageNumber = (page ?? 1);


            return View(models.ToPagedList(pageNumber, pageSize));


        }

        // GET: Product/Details/5
        public async Task<ActionResult> Details(int id)
        {
            
            var getDetails = await service.GetDetailsAsync(id);

            if (getDetails == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductModel productModel = new ProductModel();

            productModel = Mapper.Map<IProduct, ProductModel>(getDetails);
            if (getDetails == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "ProductId", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Model,Quantity,ProductCategoryId")] ProductModel productModel)
        {
            

            if (ModelState.IsValid)
            {
                var product = Mapper.Map<ProductModel, IProduct>(productModel);
                await service.CreateProductAsync(product); 
  
                return RedirectToAction("Index");
            }

            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "ProductId", "Name", productModel.ProductCategoryId);
            return View(productModel); 
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            var product = await service.GetDetailsAsync(id); 
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            productModel = Mapper.Map<IProduct, ProductModel>(product); 
           
            if (productModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "ProductId", "Name", productModel.ProductCategoryId);
            return View(productModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Model,Quantity,ProductCategoryId")] ProductModel productModel)
        {


            if (ModelState.IsValid)
            {
                var product = Mapper.Map<ProductModel, Product>(productModel);

                await service.EditAsync(product); 
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategory, "ProductId", "Name", productModel.ProductCategoryId);
            return View(productModel);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var deleteProduct = await service.GetDetailsAsync(id); 
            if (deleteProduct == null)
            {
                return HttpNotFound();
            }
            return View(deleteProduct);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(ProductModel model)
        {
            var product = Mapper.Map<ProductModel, Product>(model); 

            if(product != null)
            {
                await service.DeleteItemAsync(product.ID);
                return RedirectToAction("Index"); 
            }
            return View(model); 

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
