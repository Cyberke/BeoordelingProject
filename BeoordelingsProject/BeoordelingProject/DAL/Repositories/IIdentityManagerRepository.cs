using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IIdentityManagerRepository
    {
        bool AddUserToRole(string userId, string roleName);
        void ClearUserRoles(string userId);
        global::Microsoft.AspNet.Identity.IdentityResult Create(global::BeoordelingProject.Models.ApplicationUser user, string password);
        global::System.Threading.Tasks.Task<global::System.Security.Claims.ClaimsIdentity> CreateIdentityAsync(global::BeoordelingProject.Models.ApplicationUser user, string auth);
        bool CreateRole(string name, string description = "");
        global::BeoordelingProject.Models.ApplicationUser FindAsync(string userName, string password);
        global::BeoordelingProject.Models.ApplicationUser GetUser(string userName);
        bool RoleExists(string name);
    }
}
