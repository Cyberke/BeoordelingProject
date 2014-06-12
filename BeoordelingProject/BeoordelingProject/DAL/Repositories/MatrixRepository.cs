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
            
            var innerquery =
            (
                from h in context.Hoofdaspecten
                where h.Rollen.Any(r => r.ID == rolID)
                select h
            );

            var query =
            (
                from m in context.Matrices
                where m.ID.Equals(matrixID)
                select m
            );

            Matrix mat = query.First();
            List<Hoofdaspect> haRol = innerquery.ToList<Hoofdaspect>();

            List<Hoofdaspect> newList = new List<Hoofdaspect>();
            for(int i = 0; i < mat.Hoofdaspecten.Count; i++)
            {
                for(int j = 0; j < haRol.Count; j++)
                {
                    if(mat.Hoofdaspecten[i] == haRol[j])
                        newList.Add(haRol[j]);
                }
            }

            mat.Hoofdaspecten = newList;

            return mat;
        }

        public int GetWegingForDeelaspect(int deelresID)
        {
            var query =
                (
                    from d in context.Deelaspect
                    where d.ID.Equals(deelresID)
                    select d.Weging
                );

            return query.First();
        }

    }
}