using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IAccountRepository
    {
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.ApplicationUser> All();
    }
}
