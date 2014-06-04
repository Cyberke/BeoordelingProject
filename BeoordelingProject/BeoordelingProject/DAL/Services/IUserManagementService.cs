using Microsoft.AspNet.Identity;
using BeoordelingProject.Models;
using System;
using System.Security.Claims;
namespace BeoordelingProject.DAL.Services
{
    public interface IUserManagementService {
        IdentityResult Create(ApplicationUser user, string password);
        ClaimsIdentity CreateIdentity(ApplicationUser user, string auth);
        ApplicationUser Find(string userName, string password);
        bool AddUserToRoleUser(string userId);
        ApplicationUser GetUser(string userName);
    }
}
