using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IUserManagementService
    {
        bool AddUserToRoleUser(string userId);
        Microsoft.AspNet.Identity.IdentityResult Create(BeoordelingProject.Models.ApplicationUser user, string password);
        System.Security.Claims.ClaimsIdentity CreateIdentity(BeoordelingProject.Models.ApplicationUser user, string auth);
        void EditUser(BeoordelingProject.Models.ApplicationUser user);
        BeoordelingProject.Models.ApplicationUser Find(string userName, string password);
        BeoordelingProject.Models.ApplicationUser GetUser(string userName);
    }
}
