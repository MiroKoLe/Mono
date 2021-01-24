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
using Project.Repository.Common;
using Project.Model;
using Project.Model.Common;
using Project.Service.Common;

namespace MVC.Controllers
{
    public class ProductAPIController : ApiController
    {

        private readonly IProductService productService;
        IPaging paging; 

        public ProductAPIController(IProductService _productService, IPaging _paging)
        {
            this.productService = _productService;
            this.paging = _paging; 
        }

        // GET: api/ProductAPI
        [HttpGet]
        [Route("api/ProductAPI/")]
        public async Task<IHttpActionResult> GetAllProducts(int? pageNumber = null, bool isAscending = false, string search = null, int? pageSize = null)
        {

            paging.PageNumber = pageNumber ?? 1;
            paging.Search = search;
            paging.IsAscending = isAscending;
            var models = await productService.GetProductsAsync(paging);

            return Ok(models); 

        }
        
            
        

        // GET: api/ProductAPI/5
        [ResponseType(typeof(ProductModel))]
        public async Task<IHttpActionResult> GetProductById(int id)
        {

            var product = await productService.GetDetailsAsync(id); 
    
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/ProductAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProductModelAsync(int id, ProductModel productModel)
        {
            var model = Mapper.Map<ProductModel, IProduct>(productModel);
            var product = await productService.CreateProductAsync(model);

            return Ok(); 
        }

        // POST: api/ProductAPI
        [ResponseType(typeof(ProductModel))]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostNewProductAsync(ProductModel productModel)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data!");
            }

            Product productDomain = new Product();

            productDomain = Mapper.Map<ProductModel, Product>(productModel);

            await productService.CreateProductAsync(productDomain);


            return Ok();
        }

        // DELETE: api/ProductAPI/5
        [ResponseType(typeof(ProductModel))]
        public async Task<IHttpActionResult> DeleteAsync(int id)
        {
            var productDomain = await productService.DeleteItemAsync(id);
          


            return Ok();


        }

       
    }
}