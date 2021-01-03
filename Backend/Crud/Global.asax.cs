using AutoMapper;
using Crud.Interface;
using Crud.Models;
using Crud.Repository;
using Crud.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Crud
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
          

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
          

            Mapper.Initialize(cfg =>
            {

                cfg.CreateMap<ProductDomain, ProductRestModel>();
                cfg.CreateMap<ProductRestModel, ProductDomain>();
                cfg.CreateMap<ProductTable, ProductDomain>();
                cfg.CreateMap<ProductDomain, ProductTable>();

            });

        }




           


    }
}

