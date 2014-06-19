using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IMatrixRepository : IGenericRepository<Matrix>
    {
        int GetDeelaspectenCountForHoofdaspect(int hoofdid);
        System.Collections.Generic.List<BeoordelingProject.Models.Hoofdaspect> GetHoofdaspectenForMatrix(int matrixid);
        BeoordelingProject.Models.Matrix GetMatrixForRol(int matrixID, int rolID);
        int GetMatrixIdByRichtingByType(bool tussentijds, string richting);
        System.Collections.Generic.List<string> GetOpleidingen();
        System.Collections.Generic.List<int> getRollenMatrix(int matid);
        int getTotaalAantalDeelaspecten(int matid);
        int GetWegingForDeelaspect(int deelresID);
        int GetWegingForHoofdaspect(int hoofdresID);
    }
}
