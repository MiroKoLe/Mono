using AutoMapper;
using Crud.Interface;
using Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Crud_Backend.Controllers
{
    public class ApiProductController : ApiController
    {
        private Sample6Entities context = new Sample6Entities();


        public ApiProductController(IProductService productService)
        {
            ProductService = productService;
        }
        public IProductService ProductService { get; set; }


        // GET: api/ApiProduct - radi
        public IHttpActionResult GetAllProducts()
        {

            var models = context.ProductTable.ToList();

            return Ok(models);



        }

        // GET: api/ApiProduct/5 - radi
        public async Task<IHttpActionResult> GetProductByIdAsync(int id)
        {
            var productDomain = await ProductService.GetDetailAsync(id);


            ProductRestModel productRestModel = new ProductRestModel();

            productRestModel = Mapper.Map<ProductDomain, ProductRestModel>(productDomain);

            return Ok(productRestModel);

            //Test Sourcetree


        }

        // POST: api/ApiProduct - Create
        public async Task<IHttpActionResult> PostNewProductAsync(ProductRestModel productRestModel)
        {



            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data!");
            }

            ProductDomain productDomain = new ProductDomain();

            productDomain = Mapper.Map<ProductRestModel, ProductDomain>(productRestModel);

            await ProductService.ProductCreateAsync(productDomain);


            return Ok();
        }

        // PUT: api/ApiProduct/5 - Edit
        public async Task<IHttpActionResult> PutProductAsync(int id, [FromBody]ProductRestModel productRestModel)
        {
            ProductDomain productDomain = new ProductDomain();

            productDomain = Mapper.Map<ProductRestModel, ProductDomain>(productRestModel);
            await ProductService.EditAsync(productDomain);

            return Ok();



        }

        // DELETE: api/ApiProduct/5
        public async Task<IHttpActionResult> DeleteAsync(int? id)
        {
            var productDomain = await ProductService.GetDeleteAsync(id);
            ProductRestModel productRestModel = new ProductRestModel();

            productRestModel = Mapper.Map<ProductDomain, ProductRestModel>(productDomain);


            return Ok();


        }
    }
}

