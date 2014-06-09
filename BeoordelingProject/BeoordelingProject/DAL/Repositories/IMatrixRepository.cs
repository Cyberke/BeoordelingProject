using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IMatrixRepository
    {
        BeoordelingProject.Models.Matrix GetMatrixByID(int id);
    }
}
