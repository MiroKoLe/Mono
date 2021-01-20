using AutoMapper;
using Project.DAL;
using Project.Model;
using Project.Model.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
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
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<ProductContext>(new DropCreateDatabaseIfModelChanges<ProductContext>());


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);



            Mapper.Initialize(cfg =>
                {

                    cfg.CreateMap<IProduct, ProductModel>();
                    cfg.CreateMap<Product, ProductModel>();
                    cfg.CreateMap<ProductModel, IProduct>();
                    cfg.CreateMap<IProduct, ProductEntity>(); 
                    cfg.CreateMap<ProductEntity, IProduct>();
                    cfg.CreateMap<ProductEntity, Product>();
                    cfg.CreateMap<ICategories, ProductCategoryModel>();




                });
        }
    }
}
