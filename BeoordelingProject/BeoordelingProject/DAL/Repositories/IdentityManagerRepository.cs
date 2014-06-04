using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BeoordelingProject.DAL.Context;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BeoordelingProject.DAL.Context;

namespace BeoordelingProject.DAL.Repositories
{
    public class IdentityManagerRepository : BeoordelingProject.DAL.Repositories.IIdentityManagerRepository
    {

        private BeoordelingsContext context = null;
        private RoleManager<ApplicationRole> roleManager = null;
        private UserManager<ApplicationUser> userManager = null;


        public IdentityManagerRepository()
        {

        }

        public IdentityManagerRepository(BeoordelingsContext context)
        {
            this.context = context;
            roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        }

        public ApplicationUser Find(string userName, string password)
        {
            return userManager.Find<ApplicationUser>(userName, password);
        }

        public ApplicationUser GetUser(string userName)
        {
            return userManager.FindByName(userName);
        }

        public bool RoleExists(string name)
        {
            return roleManager.RoleExists(name);
        }


        public IdentityResult CreateRole(string name, string description = "")
        {
            return roleManager.Create(new ApplicationRole(name, description));
        }

        public ClaimsIdentity CreateIdentity(ApplicationUser user, string auth)
        {
            return userManager.CreateIdentity<ApplicationUser>(user, auth);
        }

        public IdentityResult Create(ApplicationUser user, string password)
        {
            return userManager.Create<ApplicationUser>(user, password);
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }


        public void ClearUserRoles(string userId)
        {
            var user = userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);
            foreach (var role in currentRoles)
            {
                userManager.RemoveFromRole(userId, role.Role.Name);
            }
        }
    }

}