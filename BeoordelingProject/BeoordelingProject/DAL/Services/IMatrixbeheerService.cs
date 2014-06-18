using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IMatrixbeheerService
    {
        System.Collections.Generic.List<BeoordelingProject.Models.Hoofdaspect> GetHoofdaspectenByMatrixId(int matid);
        BeoordelingProject.Models.Matrix GetMatrixByRichtingByTussentijds(string opleiding, bool tussentijds);
        System.Collections.Generic.List<string> GetOpleidingen();
        System.Collections.Generic.List<int> getRollenMatrix(int matid);
        void UpdateMatrix(BeoordelingProject.Models.Matrix m);
    }
}
