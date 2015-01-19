[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TestPortal.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TestPortal.App_Start.NinjectWebCommon), "Stop")]

namespace TestPortal.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using TestPortal.BL;
    using TestPortal.DAL.Db;
    using TestPortal.DAL.UnitOfWork;
    using TestPortal.DAL.Entities;
    using TestPortal.DAL.Repository;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
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
            //-----services
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IGroupService>().To<GroupService>().InRequestScope();
            kernel.Bind<IStoryService>().To<StoryService>().InRequestScope();

            //------unit of work
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            //------repositories
            kernel.Bind<IRepository<User>>().To<Repository<User>>().InRequestScope();
            kernel.Bind<IRepository<Group>>().To<Repository<Group>>().InRequestScope();
            kernel.Bind<IRepository<Story>>().To<Repository<Story>>().InRequestScope();

            //------db context
            kernel.Bind<System.Data.Entity.DbContext>().To<PortalContext>().InRequestScope();
        }        
    }
}
