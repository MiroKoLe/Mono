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

        Task<IList<ICategories>> GetProductCategoriesAsync();
        Task <int>CategoryUpdateAsync(ICategories productCategory);
        Task <ICategories> GetDetailForCategory(int id);
        Task <int> CategoryEdit(ICategories productCategory);
        Task <int> DeleteCategoryAsync(int id);
    }
}
