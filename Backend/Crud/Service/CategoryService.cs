using Crud.Interface;
using Crud.Models;
using Crud.Repository;
using Crud.UOW;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Crud.Service
{
    public class CategoryService 
    {
        
        static Sample6Entities context = new Sample6Entities();

        CategoryRepository categoryRepository = new CategoryRepository();
        ProductRepository productRepository = new ProductRepository(context);
        UnitOfWork unitOfWork = new UnitOfWork(context); 



        public async Task<List<ProductCategory>> GetProductCategoriesAsync()
        {
            var categoryGet = await categoryRepository.GetProductCategoriesAsync();

            return categoryGet.ToList(); 

            
        }

        public async Task CategoryUpdateAsync()
        {
            await categoryRepository.CreateCategoryAsync(); 

        }

        public ProductCategory GetDetailForCategory(int? id)
        {

            var productDetails = categoryRepository.GetDetails(id);

            return productDetails;

        }


       public void categoryEdit(ProductCategory productCategory)
        {


            var entity = categoryRepository.GetDetails(productCategory.ProductId);

            var active = entity.IsActive;


            entity.ProductId = productCategory.ProductId;
            entity.IsActive = productCategory.IsActive;
            entity.Name = productCategory.Name;

            
            
            categoryRepository.EditCategory(entity);
       

            var productList = entity.ProductTable;
            entity.ProductTable = null;


            if (entity.IsActive != active)
            {

                foreach (var product in productList)
                {


                    var entityToUpdate =  productRepository.GetDetails(product.ID);


                    productRepository.ProductEdit(entityToUpdate);

                }

            }


        }
        
    }

}