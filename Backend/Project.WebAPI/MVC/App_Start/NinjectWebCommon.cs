[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MVC.App_Start.NinjectWebCommon), "Stop")]

namespace MVC.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Project.Common;
    using Project.Common.Interface;
    using Project.DAL;
    using Project.Model;
    using Project.Model.Common;
    using Project.Repository;
    using Project.Repository.Common;
    using Project.Service;
    using Project.Service.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);


                return kernel;



            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IProductCategoryService>().To<ProductCategoryService>();
            kernel.Bind<IProductRepository>().To<ProductRepository>();
            kernel.Bind<IProductCategoryRepository>().To<ProductCategoryRepository>();
            kernel.Bind<IProduct>().To<Product>();
            kernel.Bind<ICategories>().To<ProductCategory>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IRepository<ProductEntity>>().To<GenericRepository<ProductEntity>>();
            kernel.Bind<IRepository<ProductCategoryEntity>>().To<GenericRepository<ProductCategoryEntity>>();
            kernel.Bind<IAscending>().To<Ascending>();
            kernel.Bind<ICount>().To<Count>();
            kernel.Bind<IItemSearch>().To<ItemSearch>();
            kernel.Bind<IPageNumber>().To<PageNumber>();
            kernel.Bind<ISize>().To<Size>();
        }
    }
}