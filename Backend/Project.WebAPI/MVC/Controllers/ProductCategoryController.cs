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
    public class ProductCategoryController : Controller
    {

        private readonly IProductCategoryService categoryService;
        IAscending ascending;
        ICount count;
        IPageNumber number;
        ISize size;
        IItemSearch itemSearch;

        public ProductCategoryController(IProductCategoryService _categoryService, IAscending _ascending, ICount _count, IPageNumber _number, ISize _size, IItemSearch _itemSearch)
        {
            this.categoryService = _categoryService;
            this.ascending = _ascending;
            this.count = _count;
            this.number = _number;
            this.size = _size;
            this.itemSearch = _itemSearch;

        }


        // GET: ProductCategory
        [HttpGet]
        public async Task<ActionResult> Index(string search, int? pageNumber, bool isAscending = false)
        {

            itemSearch.Search = search;
            number.pageNumber = pageNumber ?? 1;
            ascending.IsAscending = isAscending;


            List<ProductCategoryModel> model = new List<ProductCategoryModel>();
            var pagedList = await categoryService.GetProductCategoriesAsync(ascending, count, number, size, itemSearch);
            var newPagedList = pagedList.ToList();
            ViewBag.sortOrder = isAscending ? false : true;
            model = Mapper.Map<List<ICategories>, List<ProductCategoryModel>>(newPagedList);
            var paged = new StaticPagedList<ProductCategoryModel>(model, pageNumber ?? 1, size.PageSize, count.TotalCount);
            return View(paged);

        }



        // GET: ProductCategory/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var getDetails = await categoryService.GetDetailForCategory(id);

            ProductCategoryModel categoryModel = new ProductCategoryModel();

            categoryModel = Mapper.Map<ICategories, ProductCategoryModel>(getDetails);
            if (getDetails == null)
            {
                return HttpNotFound();
            }
            return View(categoryModel);
        }

        // GET: ProductCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ProductId,Name")] ProductCategoryModel productCategoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<ProductCategoryModel, ICategories>(productCategoryModel);
                await categoryService.CategoryUpdateAsync(category);
                return RedirectToAction("Index");
            }

            return View(productCategoryModel);
        }

        // GET: ProductCategory/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            ProductCategoryModel categoryModel = new ProductCategoryModel();
            var category = await categoryService.GetDetailForCategory(id);
            if (category == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            categoryModel = Mapper.Map<ICategories, ProductCategoryModel>(category);

            if (categoryModel == null)
            {
                return HttpNotFound();
            }

            return View(categoryModel);
        }


        // POST: ProductCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ProductId,Name")] ProductCategoryModel productCategoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = Mapper.Map<ProductCategoryModel, ProductCategory>(productCategoryModel);
                await categoryService.CategoryEdit(category);
                return RedirectToAction("Index");
            }
            return View(productCategoryModel);
        }

        // GET: ProductCategory/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            ProductCategoryModel categoryModel = new ProductCategoryModel();

            var category = await categoryService.GetDetailForCategory(id);

            if (category == null)
            {
                return HttpNotFound();
            }


            categoryModel = Mapper.Map<ICategories, ProductCategoryModel>(category);

            return View(categoryModel);
        }

        // POST: ProductCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync(int id)
        {

            await categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");


        }
    }
}