using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IMatrixRepository : IGenericRepository<Matrix>
    {
        System.Collections.Generic.List<BeoordelingProject.Models.Hoofdaspect> GetHoofdaspectenForMatrix(int matrixid);
        BeoordelingProject.Models.Matrix GetMatrixForRol(int matrixID, int rolID);
        int GetWegingForDeelaspect(int deelresID);
        int GetWegingForHoofdaspect(int hoofdresID);
    }
}
