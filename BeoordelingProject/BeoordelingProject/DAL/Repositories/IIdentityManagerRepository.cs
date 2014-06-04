using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IIdentityManagerRepository
    {
        bool AddUserToRole(string userId, string roleName);
        void ClearUserRoles(string userId);
        Microsoft.AspNet.Identity.IdentityResult Create(BeoordelingProject.Models.ApplicationUser user, string password);
        System.Security.Claims.ClaimsIdentity CreateIdentity(BeoordelingProject.Models.ApplicationUser user, string auth);
        Microsoft.AspNet.Identity.IdentityResult CreateRole(string name, string description = "");
        BeoordelingProject.Models.ApplicationUser Find(string userName, string password);
        BeoordelingProject.Models.ApplicationUser GetUser(string userName);
        bool RoleExists(string name);
    }
}
