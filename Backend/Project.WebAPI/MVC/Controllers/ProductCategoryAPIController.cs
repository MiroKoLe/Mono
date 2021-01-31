using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using MVC.Models;
using Project.Common;
using Project.Common.Interface;
using Project.DAL;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;

namespace MVC.Controllers
{
   
    public class ProductCategoryAPIController : ApiController
    {
        private readonly IProductCategoryService categoryService;
        IAscending ascending;
        ICount count;
        IPageNumber number;
        ISize size;
        IItemSearch itemSearch;

        public ProductCategoryAPIController(IProductCategoryService _categoryService, IAscending _ascending, ICount _count, IPageNumber _number, ISize _size, IItemSearch _itemSearch)
        {

            this.categoryService = _categoryService;
            this.ascending = _ascending;
            this.count = _count;
            this.number = _number;
            this.size = _size;
            this.itemSearch = _itemSearch;
        }

        // GET: api/ProductCategoryAPI
        [HttpGet]
        [Route("api/ProductCategoryAPI/")]
        public async Task<IHttpActionResult> IndexAsync(int? pageNumber = null, bool isAscending = false, string search = null, int? pageSize = null)
        {


            itemSearch.Search = search;
            number.pageNumber = pageNumber ?? 1;
            ascending.IsAscending = isAscending;


            var pagedList = await categoryService.GetProductCategoriesAsync(ascending, count, number, size, itemSearch);
            return Ok(pagedList);



        }

        [ResponseType(typeof(ProductCategoryModel))]
        public async Task<IHttpActionResult> GetProductByIdAsync(int id)
        {

            var product = await categoryService.GetDetailForCategory(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/ProductAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductModelAsync(int id, ProductCategoryModel productModel)
        {
            var model = Mapper.Map<ProductCategoryModel, ICategories>(productModel);
            var product = await categoryService.CategoryUpdateAsync(model);

            return Ok();
        }

        // POST: api/ProductAPI
        [ResponseType(typeof(ProductCategoryModel))]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostNewProductAsync(ProductCategoryModel productModel)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data!");
            }

            ProductCategory productDomain = new ProductCategory();

            productDomain = Mapper.Map<ProductCategoryModel, ProductCategory>(productModel);

            await categoryService.CategoryUpdateAsync(productDomain);


            return Ok();
        }

        // DELETE: api/ProductAPI/5
        [ResponseType(typeof(ProductCategoryModel))]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            var productDomain = await categoryService.DeleteCategoryAsync(id);



            return Ok();


        }


    }
}