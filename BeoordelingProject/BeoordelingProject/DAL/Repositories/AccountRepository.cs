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

        public ApplicationUser GetAdmin()
        {
            var query =
            (
                from u in context.Users
                from r in u.Roles

                where u.Id.Equals(r.UserId)
                where r.Role.Name.Equals("Admin")

                select u
            );

            return query.First();
        }

        public void DeleteGebruiker(ApplicationUser user)
        {
            /*
            string sql = "DELETE FROM studentrollens AS sr ";
            sql += "INNER JOIN applicationuserstudentrollens AS ausr ";
            sql += "ON sr.rol_id = ausr.studentrollen_rol_id ";
            sql += "WHERE applicationuser_id = " + user.Id;




            context.Users.Remove(user);
            context.SaveChanges();
            */
            
            var query = 
            (
                from u in context.Users
                from sr in u.StudentRollen

                where u.Id.Equals(user.Id)

                select u
            );

            ApplicationUser lel = query.First();

            for (int i = query.First().StudentRollen.Count - 1; i >= 0; i-- )
            {
                context.StudentRollen.Remove(query.First().StudentRollen[i]);
            }
            
            context.Users.Remove(query.First());
            context.SaveChanges();
        }
    }
}