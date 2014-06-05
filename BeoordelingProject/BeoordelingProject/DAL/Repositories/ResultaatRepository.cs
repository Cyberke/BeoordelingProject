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

        public IEnumerable<Resultaat> GetEindResultaten(int id)
        {

            return null;
        }

    }
}