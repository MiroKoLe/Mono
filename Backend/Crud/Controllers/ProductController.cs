using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Crud.Models;
using Crud.Repository;
using Crud.Service;
using PagedList;
using AutoMapper;
using Crud.Interface;
using Ninject;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing.Printing;



namespace Crud.Controllers
{
   
    public class ProductController : Controller
    {

        private Sample6Entities context = new Sample6Entities();


        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }
        public IProductService ProductService { get; set; }



        // GET: Product
        public async Task<ActionResult> Index(string searchBy, string search, int? page, string sortBy, int? PageSize, int? PageNumber)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";
            ViewBag.SortModelParameter = sortBy == "Model" ? "Model desc" : "Model";

            var productDomain = await ProductService.GetProductsAsync(searchBy, search, sortBy, PageSize, page, PageNumber);
            List<ProductRestModel> models = new List<ProductRestModel>();

            models = Mapper.Map<List<ProductDomain>, List<ProductRestModel>>(productDomain);

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            

            return View(models.ToPagedList(pageNumber, pageSize)); 


        }


        // GET: Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            var productDomain = await ProductService.GetDetailAsync(id);
         

            ProductRestModel productRestModel = new ProductRestModel();

            productRestModel = Mapper.Map<ProductDomain, ProductRestModel>(productDomain); 
         

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            if (productRestModel == null)
            {
                return HttpNotFound();
            }
            return View(productRestModel);
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.ProductCategoryId = new SelectList(context.ProductCategory, "ProductId", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Model,Quantity,ProductCategoryId")] ProductRestModel productRestModel)
        {
            ProductDomain productDomain = new ProductDomain();

            productDomain = Mapper.Map<ProductRestModel, ProductDomain>(productRestModel);

            try
            {
                if (ModelState.IsValid)
                {

                    await ProductService.ProductCreateAsync(productDomain);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Unable To Save Changes, Please Try Again");
            }



            ViewBag.ProductCategoryId = new SelectList(context.ProductCategory, "ProductId", "Name", productDomain.ProductCategoryId);
            return View(productRestModel);
        }

        // GET: Product/EditAsync/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var productDomain = await ProductService.GetDetailAsync(id);

            ProductRestModel productRestModel = new ProductRestModel();
            productRestModel = Mapper.Map<ProductDomain, ProductRestModel>(productDomain);

            
            if (productDomain == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductCategoryId = new SelectList(context.ProductCategory, "ProductId", "Name", productDomain.ProductCategoryId);
            return View(productRestModel);
        }


        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> EditAsync([Bind(Include = "ID,Name,Model,Quantity,ProductCategoryId")] ProductRestModel productRestModel)
        {

            ProductDomain productDomain = new ProductDomain();

            productDomain = Mapper.Map<ProductRestModel, ProductDomain>(productRestModel);

            if (ModelState.IsValid)
            {

                await ProductService.EditAsync(productDomain);
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategoryId = new SelectList(context.ProductCategory, "ProductId", "Name", productRestModel.ProductCategoryId);
            return View(productRestModel);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var productDomain = await ProductService.GetDetailAsync(id);  

            if (productDomain == null)
            {
                return HttpNotFound();
            }

            ProductRestModel productRestModel = new ProductRestModel();

            productRestModel = Mapper.Map<ProductDomain, ProductRestModel>(productDomain);
            return View(productRestModel);
        }



        // POST: Product/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {

            var productDomain = await ProductService.GetDeleteAsync(id);
            ProductRestModel productRestModel = new ProductRestModel();

            productRestModel = Mapper.Map<ProductDomain, ProductRestModel>(productDomain);

            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
