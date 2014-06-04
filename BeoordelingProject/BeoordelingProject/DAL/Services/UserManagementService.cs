using Microsoft.AspNet.Identity;
using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace BeoordelingProject.DAL.Services {
    public class UserManagementService : IUserManagementService {

        private IIdentityManagerRepository identityRepository = null;

        public UserManagementService() {

        }

        public UserManagementService(IIdentityManagerRepository repo) {
            this.identityRepository = repo;
        }

        public ApplicationUser Find(string userName, string password) {
            return identityRepository.Find(userName, password);
        }

        public ClaimsIdentity CreateIdentity(ApplicationUser user, string auth) {
            return identityRepository.CreateIdentity(user, auth);
        }

        public IdentityResult Create(ApplicationUser user, string password) {
            return identityRepository.Create(user, password);
        }

        public bool AddUserToRoleUser(string userId) {
            return identityRepository.AddUserToRole(userId, "User");
        }

        public ApplicationUser GetUser(string userName) {
            return identityRepository.GetUser(userName);
        }
    }
}