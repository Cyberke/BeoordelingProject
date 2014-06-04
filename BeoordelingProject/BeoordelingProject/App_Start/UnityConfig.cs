using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BeoordelingProject.DAL.Services;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using BeoordelingProject.DAL.Context;
using BeoordelingProject.Models;

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
            container.RegisterType<IIdentityManagerRepository, IdentityManagerRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserManagementService, UserManagementService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentRepository, StudentRepository>(new HierarchicalLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}