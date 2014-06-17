using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IAccountRepository : IGenericRepository<ApplicationUser>
    {
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.ApplicationUser> All();
        void DeleteGebruiker(BeoordelingProject.Models.ApplicationUser user);
        BeoordelingProject.Models.ApplicationUser GetAdmin();
    }
}
