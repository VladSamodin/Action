using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Services;
using DAL;
using DAL.Interface;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using DAL.Concrete;
using Ninject;
using Ninject.Web.Common;
using System.Data.Entity;
using ORM;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<EntityModel>().InRequestScope().WithConstructorArgument("EntityModel");
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<EntityModel>().InSingletonScope();
            }

            #region Repositories
            kernel.Bind<IRepository<DalBid>>().To<BidRepository>();
            kernel.Bind<IRepository<DalCategory>>().To<CategoryRepository>();
            kernel.Bind<IRepository<DalLot>>().To<LotRepository>();
            //kernel.Bind<IRepository<DalRole>>().To<RoleRepository>();
            //kernel.Bind<IRepository<DalUser>>().To<UserRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRepository<DalModerationStatus>>().To<ModerationStatusRepository>();
            #endregion

            #region Services
            kernel.Bind<IService<BllBid>>().To<BidService>();
            //kernel.Bind<IService<BllLot>>().To<LotService>();
            kernel.Bind<ILotService>().To<LotService>();
            kernel.Bind<IService<BllCategory>>().To<CategoryService>();
            //kernel.Bind<IService<BllRole>>().To<RoleService>();
            //kernel.Bind<IService<BllUser>>().To<UserService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IService<BllModerationStatus>>().To<ModerationStatusService>();
            #endregion
        }
    }
}