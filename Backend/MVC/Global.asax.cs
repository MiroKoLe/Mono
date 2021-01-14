using AutoMapper;
using Project.Model;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            Mapper.Initialize(cfg =>
                {

                    cfg.CreateMap<ProductModel, Product>();
                    cfg.CreateMap<Product, ProductModel>();
                    cfg.CreateMap<IProduct, ProductModel>();
                    cfg.CreateMap<ProductModel, IProduct>();
                    cfg.CreateMap<List<IProduct>, IList<ProductModel>>(); 

                });
        }
    }
}
