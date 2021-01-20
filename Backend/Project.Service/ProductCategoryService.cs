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

        readonly private IProductCategoryRepository categoryRepository; 

        public ProductCategoryService(IProductCategoryRepository _categoryRepository)
        {
            this.categoryRepository = _categoryRepository; 
        }

        public async Task<IList<ICategories>> GetProductCategoriesAsync()
        {

            return await categoryRepository.GetProductsAsync();

         
        }

        public async Task<int> CategoryUpdateAsync(ICategories category)
        {
            return await categoryRepository.CreateProductAsync(category);

        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            return await categoryRepository.DeleteItemAsync(id); 
        }

        public async Task<int> CategoryEdit(ICategories category)
        {
            return await categoryRepository.EditAsync(category); 
        }

        public async Task<ICategories> GetDetailForCategory(int id)
        {
            return await categoryRepository.GetDetailsAsync(id); 
        }




    }
}
