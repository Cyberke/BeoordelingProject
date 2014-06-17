using BeoordelingProject.DAL.Repositories;
using BeoordelingProject.DAL.UnitOfWork;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Services {
    public class AdministratorService : BeoordelingProject.DAL.Services.IAdministratorService
    {
        IUnitOfWork uow = null;
        IGenericRepository<ApplicationUser> adminRepository = null;
        IIdentityManagerRepository identityManagerRepository = null;
        IAccountRepository accountRepository = null;

        public AdministratorService() {
        }

        public AdministratorService(IUnitOfWork uow,
            IGenericRepository<ApplicationUser> adminRepository,
            IIdentityManagerRepository identityManagerRepository,
            IAccountRepository accountRepository) {
            this.uow = uow;
            this.adminRepository = adminRepository;
            this.identityManagerRepository = identityManagerRepository;
            this.accountRepository = accountRepository;
        }

        public ApplicationUser GetAdminById(string id) {
            return adminRepository.GetByID(id);
        }

        public ApplicationUser GetAdminByUserName(string userName) {
            return identityManagerRepository.GetUser(userName);
        }

        public void UpdateAdmin(ApplicationUser admin) {
            adminRepository.Update(admin);

            uow.SaveChanges();
        }

        public ApplicationUser GetAdmin()
        {
            return accountRepository.GetAdmin();
        }
    }
}