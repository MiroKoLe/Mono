using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Crud.Interface;
using Crud.Models;
using Crud.Repository;
using Crud.Service;
using PagedList;

namespace Crud.Interface
{
    public interface IProductService
    {
        Task<List<ProductDomain>> GetProductsAsync(string searchBy, string search, string sortBy, int? PageSize, int? page, int? PageNumber);

        Task ProductCreateAsync(ProductDomain domain);

        Task<ProductDomain> GetDetailAsync(int? id);

        Task EditAsync(ProductDomain productDomain);

        Task<ProductDomain> GetDeleteAsync(int? id); 


    }

}
