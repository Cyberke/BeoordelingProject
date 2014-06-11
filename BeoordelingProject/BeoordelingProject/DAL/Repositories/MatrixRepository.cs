using BeoordelingProject.DAL.Context;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Repositories
{
    public class MatrixRepository : GenericRepository<Matrix>, BeoordelingProject.DAL.Repositories.IMatrixRepository
    {
        public MatrixRepository(BeoordelingsContext context) : base(context)
        {

        }

        public Matrix GetMatrixByID(int id)
        {
            var query =
            (
                from m in context.Matrices
                from h in m.Hoofdaspecten
                from d in h.Deelaspecten
            
                where m.ID.Equals(id)

                select m
            );

            Matrix mat = query.First();

            return query.First();
        }

        public Matrix GetMatrixForRol(int matrixID, int rolID)
        {
            
            //var query =
            //(
            //    from m in context.Matrices
            //    join h in context.Hoofdaspecten
            //    on m.Hoofdaspecten equals h.ID

                
            //    //where m.ID.Equals(matrixID) && h.Rollen.Any(r => r.ID == rolID)

            //    //select m
            //    select new NewMat { M = m, H = h};
            //);

            //Matrix mat = query.First();

            return null;
            
        }

    }
}