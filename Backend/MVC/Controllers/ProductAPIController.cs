using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MVC;
using Project.Service.Common;

namespace MVC.Controllers
{
    public class ProductAPIController : ApiController
    {
        private Products db = new Products();

        private readonly IProductService productService;

        public ProductAPIController(IProductService _productService)
        {
            this.productService = _productService; 
        }

        // GET: api/ProductAPI
        public IQueryable<ProductModel> GetProductTable()
        {
            return db.ProductTable;
        }

        // GET: api/ProductAPI/5
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult GetProductModel(int id)
        {
            ProductModel productModel = db.ProductTable.Find(id);
            if (productModel == null)
            {
                return NotFound();
            }

            return Ok(productModel);
        }

        // PUT: api/ProductAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductModel(int id, ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productModel.ID)
            {
                return BadRequest();
            }

            db.Entry(productModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProductAPI
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult PostProductModel(ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductTable.Add(productModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productModel.ID }, productModel);
        }

        // DELETE: api/ProductAPI/5
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult DeleteProductModel(int id)
        {
            ProductModel productModel = db.ProductTable.Find(id);
            if (productModel == null)
            {
                return NotFound();
            }

            db.ProductTable.Remove(productModel);
            db.SaveChanges();

            return Ok(productModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductModelExists(int id)
        {
            return db.ProductTable.Count(e => e.ID == id) > 0;
        }
    }
}