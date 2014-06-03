using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BeoordelingProject.DAL.Services;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.DAL.Repositories;

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
            container.RegisterType<IStudentService, StudentService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager());
            container.RegisterType<IStudentRepository, StudentRepository>(new HierarchicalLifetimeManager());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}