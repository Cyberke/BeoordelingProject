using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IBeoordelingsService
    {
        void CreateBeoordeling(BeoordelingProject.ViewModel.BeoordelingsVM vm);
        System.Collections.Generic.List<BeoordelingProject.Models.DeelaspectResultaat> FillDeelaspectResultaten(BeoordelingProject.Models.Matrix m, System.Collections.Generic.List<double> scores);
        System.Collections.Generic.List<double> GetListDeelaspectScore(System.Collections.Generic.List<BeoordelingProject.Models.DeelaspectResultaat> deelreslist);
        System.Collections.Generic.List<int> GetListDeelaspectWegingen(System.Collections.Generic.List<BeoordelingProject.Models.DeelaspectResultaat> deelreslist);
        System.Collections.Generic.List<double> GetListHoofdAspectScore(System.Collections.Generic.List<BeoordelingProject.Models.HoofdaspectResultaat> hoofdreslist);
        System.Collections.Generic.List<int> GetListHoofdAspectWegingen(System.Collections.Generic.List<BeoordelingProject.Models.HoofdaspectResultaat> hoofdreslist);
        BeoordelingProject.Models.Matrix GetMatrix(int id);
        BeoordelingProject.Models.Matrix GetMatrixForRol(int matrixID, int rolID);
        int GetMatrixIdByRichtingByType(bool type, string richting);
        BeoordelingProject.Models.Resultaat getResultaatByStudentId(int studentid);
        System.Collections.Generic.List<BeoordelingProject.Models.Resultaat> GetResultaten();
        System.Collections.Generic.List<BeoordelingProject.Models.Resultaat> GetTussentijdseResultaten(int id);
        bool isCFaanwezig(int studentid);
        void stuurBreekpuntMail(int studentId);
        void stuurMail(int studentId, object pdf);
    }
}
