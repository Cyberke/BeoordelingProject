using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IMatrixRepository
    {
        BeoordelingProject.Models.Matrix GetMatrixByID(int id);
        BeoordelingProject.Models.Matrix GetMatrixForRol(int matrixID, int rolID);
        int GetWegingForDeelaspect(int deelresID);
    }
}
