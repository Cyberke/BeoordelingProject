using BeoordelingProject.DAL.Context;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Repositories
{
    public class StudentRepository : GenericRepository<Student>, BeoordelingProject.DAL.Repositories.IStudentRepository
    {
        public StudentRepository(BeoordelingsContext context):base(context)
        {

        }

        public override IEnumerable<Student> All()
        {
            //alle studenten uit de DB halen
            var query = (from s in context.Studenten
                         select s);

            return query;
        }


        public IEnumerable<String> GetOpleidingen()
        {
            var query = (from s in context.Studenten
                         select s.Opleiding).Distinct();

                return query;
        }

        public IEnumerable<Rol> GetRoles()
        {
            var query = (from r in context.Rollen
                         select r);

            return query;
        }
    }
}