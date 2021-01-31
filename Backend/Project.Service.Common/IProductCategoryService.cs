using PagedList;
using Project.Common;
using Project.Common.Interface;
using Project.Model.Common;
using Project.Repository;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Common
{
    public interface IProductCategoryService
    {

        Task<IPagedList<ICategories>> GetProductCategoriesAsync(IAscending ascending, ICount count, IPageNumber number, ISize size, IItemSearch itemSearch);
        Task<int> CategoryUpdateAsync(ICategories productCategory);
        Task<ICategories> GetDetailForCategory(int id);
        Task<int> CategoryEdit(ICategories productCategory);
        Task<int> DeleteCategoryAsync(int id);
    }
}
