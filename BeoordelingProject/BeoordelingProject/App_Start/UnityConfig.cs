using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BeoordelingProject.DAL.Services;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using BeoordelingProject.DAL.Context;
using BeoordelingProject.Models;
using BeoordelingProject.Engine;

namespace BeoordelingProject
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IdentityDbContext<ApplicationUser>, BeoordelingsContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());

            //SERVICES
            container.RegisterType<IUserManagementService, UserManagementService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IBeoordelingsService, BeoordelingsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentrolService, StudentrolService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAdministratorService, AdministratorService>(new HierarchicalLifetimeManager());

            //REPOSITORIES
            container.RegisterType<IStudentRepository, StudentRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccountRepository, AccountRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IIdentityManagerRepository, IdentityManagerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IMatrixRepository, MatrixRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IResultaatRepository, ResultaatRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IGenericRepository<Rol>, GenericRepository<Rol>>(new HierarchicalLifetimeManager());
            container.RegisterType<IGenericRepository<ApplicationUser>, GenericRepository<ApplicationUser>>(new HierarchicalLifetimeManager());

            
            //ENGINE
            container.RegisterType<IBeoordelingsEngine, BeoordelingsEngine>(new HierarchicalLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}