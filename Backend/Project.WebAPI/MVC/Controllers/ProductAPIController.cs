using AutoMapper;
using MVC.Models;
using Project.Common;
using Project.Common.Interface;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace MVC.Controllers
{
    public class ProductAPIController : ApiController
    {

        private readonly IProductService productService;
        IAscending ascending;
        ICount count;
        IPageNumber number;
        ISize size;
        IItemSearch itemSearch;

        public ProductAPIController(IProductService _productService, IAscending _ascending, ICount _count, IPageNumber _number, ISize _size, IItemSearch _itemSearch)
        {
            this.productService = _productService;
            this.ascending = _ascending;
            this.count = _count;
            this.number = _number;
            this.size = _size;
            this.itemSearch = _itemSearch;
        }

        // GET: api/ProductAPI
        [HttpGet]
        [Route("api/ProductAPI/")]
        public async Task<IHttpActionResult> GetAllProducts(int? pageNumber = null, bool isAscending = false, string search = null, int? pageSize = null)
        {

            itemSearch.Search = search;
            number.pageNumber = pageNumber ?? 1;
            ascending.IsAscending = isAscending;
            var models = await productService.GetProductsAsync(ascending, count, number, size, itemSearch);

            return Ok(models);

        }




        // GET: api/ProductAPI/5
        [ResponseType(typeof(ProductModel))]
        public async Task<IHttpActionResult> GetProductByIdAsync(int id)
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