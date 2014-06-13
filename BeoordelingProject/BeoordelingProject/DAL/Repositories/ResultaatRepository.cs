using BeoordelingProject.DAL.Context;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Repositories
{
    public class ResultaatRepository: GenericRepository<Resultaat>, BeoordelingProject.DAL.Repositories.IResultaatRepository
    {
        public ResultaatRepository(BeoordelingsContext context): base(context)
        {

        }

        public IEnumerable<Resultaat> GetTussentijdseResultaten(int id)
        {
            var query =
            (
                from r in context.Resultaten
                from d in r.DeelaspectResultaten
                
                join m in context.Matrices on r.TussentijdseId equals m.ID
                select r
            );

            return query;
        }

        public List<string> CheckIfRolesCompleted(int studentid)
        {
            var query =
            (
                from r in context.Resultaten
                from hr in r.HoofdaspectResultaten

                where r.StudentId.Equals(studentid)

                select hr.Rol.Naam
            );

            List<string> namen = query.Distinct().ToList<string>();

            return namen;
        }

        public IEnumerable<Resultaat> GetEindResultaten(int id)
        {

            return null;
        }

        public int ifExistsGetStudentId(int studentid)
        {
            var query =
            (
                from r in context.Resultaten
                where r.StudentId.Equals(studentid)
                select r.StudentId
            );

            if(query.Count() != 0)
            {
                return query.First();
            }
            else
            {
                return 0;
            }
        }

        public Resultaat getByStudentId(int studentid)
        {
            var query =
            (
                from r in context.Resultaten
                where r.StudentId.Equals(studentid)
                select r
            );
            
            return query.First();
        }
    }
}