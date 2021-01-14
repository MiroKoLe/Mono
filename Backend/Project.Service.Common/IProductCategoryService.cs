using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IProductCategoryService
    {

        Task<IList<IProductCategory>> GetProductCategoriesAsync();
        Task <int>CategoryUpdateAsync(IProductCategory productCategory);
        Task <IProductCategory> GetDetailForCategory(int id);
        Task <int> CategoryEdit(IProductCategory productCategory);
        Task <int> DeleteCategoryAsync(int id);
    }
}
