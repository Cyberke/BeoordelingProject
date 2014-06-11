using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IAccountRepository: IGenericRepository<ApplicationUser>
    {
        void DeleteGebruiker(string selectedId);
    }
}
