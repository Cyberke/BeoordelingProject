using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IMatrixbeheerService
    {
        BeoordelingProject.Models.Matrix GetMatrixByRichtingByTussentijds(string opleiding, bool tussentijds);
        System.Collections.Generic.List<string> GetOpleidingen();
    }
}
