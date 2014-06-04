using System;
namespace BeoordelingProject.DAL.Services {
    public interface IBeoordelingsService {
        void CreateBeoordeling();
        BeoordelingProject.Models.Matrix GetMatrix();
        System.Collections.Generic.List<BeoordelingProject.Models.Resultaat> GetResultaten();
    }
}
