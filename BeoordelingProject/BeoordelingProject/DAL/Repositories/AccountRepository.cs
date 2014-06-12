using BeoordelingProject.DAL.Context;
using BeoordelingProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Repositories
{
    public class AccountRepository : GenericRepository<ApplicationUser>, BeoordelingProject.DAL.Repositories.IAccountRepository
    {
        public AccountRepository(BeoordelingsContext context):base(context)
        {

        }

        public override IEnumerable<ApplicationUser> All()
        {
            var query =
            (
                from u in context.Users
                from r in u.Roles

                where u.Id.Equals(r.UserId)
                where r.Role.Name.Equals("User")
                
                select u
            );

            return query;
        }

        public void DeleteGebruiker(string selectedId)
        {
            ApplicationUser user = context.Users.First(u => u.Id == selectedId);
            
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}