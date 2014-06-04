using BeoordelingProject.DAL.Context;
using BeoordelingProject.Models;
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
            //alle applicationusers uit de DB halen
            var query = (from a in context.Users
                         select a);

            return query;
        }
    }
}