using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IBeoordelingsService
    {
        void CreateBeoordeling(BeoordelingProject.Models.Resultaat res);
        System.Collections.Generic.List<BeoordelingProject.Models.DeelaspectResultaat> FillDeelaspectResultaten(BeoordelingProject.Models.Matrix m, System.Collections.Generic.List<BeoordelingProject.Models.DeelaspectResultaat> scores);
        BeoordelingProject.Models.Matrix GetMatrix(int id);
        BeoordelingProject.Models.Matrix GetMatrixForRol(int matrixID, int rolID);
        System.Collections.Generic.List<BeoordelingProject.Models.Resultaat> GetResultaten();
        System.Collections.Generic.List<BeoordelingProject.Models.Resultaat> GetTussentijdseResultaten(int id);
    }
}
