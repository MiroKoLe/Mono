using PagedList;
using Project.Common;
using Project.Common.Interface;
using Project.Model.Common;
using Project.Repository;
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

    public async Task<IPagedList<ICategories>> GetProductCategoriesAsync(IAscending ascending, ICount count, IPageNumber number, ISize size, IItemSearch itemSearch)
    {

        return await categoryRepository.GetProductsAsync(ascending, count, number, size, itemSearch);


    }

    public async Task<int> CategoryUpdateAsync(ICategories category)
    {
     if (category != null
         && category.ProductId != null
         && string.IsNullOrEmpty(category.Name))
            { 
                throw new ArgumentNullException("category");
            }

            return await categoryRepository.CreateProductAsync(category);

    }

    public async Task<int> DeleteCategoryAsync(int id)
    {
         if(id != null)
            {
                throw new ArgumentNullException("id"); 
            }
        return await categoryRepository.DeleteItemAsync(id);
    }

    public async Task<int> CategoryEdit(ICategories category)
    {
        return await categoryRepository.EditAsync(category);
    }

    public async Task<ICategories> GetDetailForCategory(int id)
    {
            if (id != null)
            {
                throw new ArgumentNullException("id");
            }
            return await categoryRepository.GetDetailsAsync(id);
    }




}
}
