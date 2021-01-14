using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class ProductCategoryService : IProductCategoryService
    {

        readonly private IProductCategoryRepository CategoryRepository; 

        public ProductCategoryService(IProductCategoryRepository categoryRepository)
        {
            this.CategoryRepository = categoryRepository; 
        }

        public async Task<IList<IProductCategory>> GetProductCategoriesAsync()
        {

            return await CategoryRepository.GetProductsAsync();

         
        }

        public async Task<int> CategoryUpdateAsync(IProductCategory category)
        {
            return await CategoryRepository.CreateProductAsync(category);

        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            return await CategoryRepository.DeleteItemAsync(id); 
        }

        public async Task<int> CategoryEdit(IProductCategory category)
        {
            return await CategoryRepository.EditAsync(category); 
        }

        public async Task<IProductCategory> GetDetailForCategory(int id)
        {
            return await CategoryRepository.GetDetailsAsync(id); 
        }




    }
}
