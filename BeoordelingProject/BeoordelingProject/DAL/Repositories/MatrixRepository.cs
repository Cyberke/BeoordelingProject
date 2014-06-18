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

        public int GetDeelaspectenCountForHoofdaspect(int hoofdid)
        {
            var query =
            (
                from h in context.Hoofdaspecten
                from d in h.Deelaspecten

                where h.ID.Equals(hoofdid)

                select d.ID
            );

            int count = query.ToList<int>().Count();
            return count;
        }

        public List<Hoofdaspect> GetHoofdaspectenForMatrix(int matrixid)
        {
            var query =
            (
                from m in context.Matrices
                from h in m.Hoofdaspecten

                where m.ID.Equals(matrixid)

                select h
            );

            return query.ToList<Hoofdaspect>();
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

        public int GetWegingForHoofdaspect(int hoofdresID)
        {
            var query =
            (
                from h in context.Hoofdaspecten
                where h.ID.Equals(hoofdresID)
                select h.Weging
            );

            return query.First();
        }

        public int GetMatrixIdByRichtingByType(bool tussentijds, string richting)
        {
            /*
            int lol = 0;

            if (tussentijds)
                lol = 1;
            else
                lol = 0;
            */
            var query =
            (
                from m in context.Matrices
                where m.Richting.Equals(richting) && m.Tussentijds.Equals(tussentijds)
                select m.ID
            );

            return query.First();
        }

        public List<string> GetOpleidingen()
        {
            var query = 
            (
                from m in context.Matrices
                select m.Richting
            );

            List<string> opleidingen = query.Distinct().ToList<string>();
            return opleidingen;
        }

        public List<int> getRollenMatrix(int matid)
        {
            var query =
            (
                from m in context.Matrices
                from h in m.Hoofdaspecten
                where m.ID.Equals(matid)
                select h
            );

            List<Hoofdaspect> hoofdaspecten = query.ToList<Hoofdaspect>();

            List<int> rollist = new List<int>();

            for(int i = 0; i < hoofdaspecten.Count * 3; i++)
            {
                rollist.Add(0);
            }

            int counter = 0;
            int id = 0;
            foreach(Hoofdaspect ho in hoofdaspecten)
            {
                List<Rol> rollen = ho.Rollen;
                foreach(Rol rol in rollen)
                {
                    switch(rol.Naam)
                    {
                        case "Promotor":
                            id = counter * 3;
                            rollist[id] = 1;
                            break;
                        case "Tweede lezer":
                            id = 1 + (counter * 3);
                            rollist[id] = 2;
                            break;
                        case "Kritische vriend":
                            id = 2 + (counter * 3);
                            rollist[2 + (counter * 3)] = 3;
                            break;
                    }
                }
                counter++;
            }

            return rollist;
        }
        
        public int getTotaalAantalDeelaspecten(int matid)
        {
            var query =
            (
                from m in context.Matrices
                from h in m.Hoofdaspecten
                from d in h.Deelaspecten

                where m.ID.Equals(matid)

                select d
            );

            int count = query.ToList<Deelaspect>().Count;
            return count;
        }

    }
}