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
using MVC.Models;
using PagedList;
using Project.Common;
using Project.Common.Interface;
using Project.DAL;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {


        private readonly IProductService service;
        IRepository<ProductCategoryEntity> categoryRepository;
        IAscending ascending;
        ICount count;
        IPageNumber number;
        ISize size;
        IItemSearch itemSearch;


        public ProductController(IProductService _service, IRepository<ProductCategoryEntity> _categoryRepository, IAscending _ascending, ICount _count, IPageNumber _number, ISize _size, IItemSearch _itemSearch )
        {
            this.service = _service;
            this.categoryRepository = _categoryRepository;
            this.ascending = _ascending;
            this.count = _count;
            this.number = _number;
            this.size = _size;
            this.itemSearch = _itemSearch;

        }

        // GET: Product
        [HttpGet]
        public async Task<ActionResult> Index(string search, int? pageNumber, bool isAscending = false)
        {

            itemSearch.Search = search;
            number.pageNumber = pageNumber ?? 1;
            ascending.IsAscending = isAscending;
            


            var product = await service.GetProductsAsync(ascending, count, number, size, itemSearch);

            List<ProductModel> productModel = new List<ProductModel>();

            var productList = product.ToList();
            ViewBag.sortOrder = isAscending ? false : true;

            productModel = Mapper.Map<List<IProduct>, List<ProductModel>>(productList);

            var pagedList = new StaticPagedList<ProductModel>(productModel, pageNumber ?? 1, size.PageSize, count.TotalCount);


            return View(pagedList);




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
            var categories = categoryRepository.GetProducts();
            ViewBag.ProductCategoryId = new SelectList(categories, "ProductId", "Name");
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(ProductModel productModel)
        {


            if (ModelState.IsValid)
            {
                var product = Mapper.Map<ProductModel, IProduct>(productModel);
                await service.CreateProductAsync(product);



                return RedirectToAction("Index");
            }
            var categories = categoryRepository.GetProducts();
            ViewBag.ProductCategoryId = new SelectList(categories, "ProductId", "Name");
            return View(productModel);
        }

        // GET: Product/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var categories = categoryRepository.GetProducts();
            ViewBag.ProductCategoryId = new SelectList(categories, "ProductId", "Name");

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

            return View(productModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProductAsync([Bind(Include = "Id,Name,Model,Quantity,ProductCategoryId")] ProductModel productModel)
        {


            if (ModelState.IsValid)
            {
                var product = Mapper.Map<ProductModel, Product>(productModel);

                await service.EditAsync(product);
                return RedirectToAction("Index");
            }

            return View(productModel);
        }

        // GET: Product/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            ProductModel productModel = new ProductModel();
            var product = await service.GetDetailsAsync(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            productModel = Mapper.Map<IProduct, ProductModel>(product);

            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {


            await service.DeleteItemAsync(id);
            return RedirectToAction("Index");



        }

    }
}