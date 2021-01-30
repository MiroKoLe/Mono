using AutoMapper;
using MVC.App_Start;
using MVC.Models;
using Project.DAL;
using Project.Model;
using Project.Model.Common;
using System.Data.Entity;
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
                cfg.CreateMap<ProductEntity, IProduct>()
                .ForMember(x => x.ProductCategory, opt => opt.MapFrom(source => source.ProductCategoryEntity));
                cfg.CreateMap<ProductEntity, Product>();
                cfg.CreateMap<ICategories, ProductCategoryModel>();
                cfg.CreateMap<ProductModel, IProduct>()
                .ForMember(x => x.Id, opt => opt.MapFrom(source => source.ProductCategoryId));
                cfg.CreateMap<ProductModel, Product>();
                cfg.CreateMap<ProductCategoryEntity, ICategories>();
                cfg.CreateMap<ProductCategoryModel, ProductCategory>()
                .ForMember(x => x.ProductTable, opt => opt.Ignore());
                cfg.CreateMap<ProductCategoryModel, ICategories>()
                .ForMember(x => x.ProductTable, opt => opt.Ignore());
                cfg.CreateMap<ProductCategory, ProductCategoryEntity>();
                cfg.CreateMap<ICategories, ProductCategoryEntity>();
                cfg.CreateMap<ProductCategoryEntity, ProductCategory>();
                cfg.CreateMap<ProductCategory, ProductCategoryModel>();
                cfg.CreateMap<ProductCategoryEntity, ICategories>();


            });
        }
    }
}
    

