using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IMatrixService
    {
        BeoordelingProject.Models.Matrix GetMatrixByID(int id);
    }
}
