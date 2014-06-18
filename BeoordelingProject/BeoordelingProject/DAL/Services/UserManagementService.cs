using Microsoft.AspNet.Identity;
using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BeoordelingProject.DAL.UnitOfWork;

namespace BeoordelingProject.DAL.Services {
    public class UserManagementService : BeoordelingProject.DAL.Services.IUserManagementService
    {

        private IIdentityManagerRepository identityRepository = null;
        IGenericRepository<ApplicationUser> userRepository = null;
        IUnitOfWork uow = null;

        public UserManagementService() {

        }

        public UserManagementService(IIdentityManagerRepository repo, IGenericRepository<ApplicationUser> userRepository, IUnitOfWork uow) {
            this.identityRepository = repo;
            this.userRepository = userRepository;
            this.uow = uow;
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

        public void EditUser(ApplicationUser user)
        {
            userRepository.Update(user);

            uow.SaveChanges();
        }
    }
}