using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IAdministratorService
    {
        BeoordelingProject.Models.ApplicationUser GetAdmin();
        BeoordelingProject.Models.ApplicationUser GetAdminById(string id);
        BeoordelingProject.Models.ApplicationUser GetAdminByUserName(string userName);
        void UpdateAdmin(BeoordelingProject.Models.ApplicationUser admin);
    }
}
