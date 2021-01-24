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
using MVC;
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
        IPaging paging;  

        public ProductCategoryAPIController(IProductCategoryService _categoryService, IPaging _paging)
        {

            this.categoryService = _categoryService;
            this.paging = _paging; 
        }

        // GET: api/ProductCategoryAPI
        [HttpGet]
        [Route("api/ProductCategoryAPI/")]
        public async Task<IHttpActionResult> Index (int? pageNumber = null, bool isAscending = false, string search = null, int? pageSize = null)
        {
         

                paging.PageNumber = pageNumber ?? 1;
                paging.Search = search;
                paging.IsAscending = isAscending;


                var pagedList = await categoryService.GetProductCategoriesAsync(paging);
                return Ok(pagedList);
    
            

        }

        [ResponseType(typeof(ProductCategoryModel))]
        public async Task<IHttpActionResult> GetProductById(int id)
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