using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IMatrixRepository
    {
        BeoordelingProject.Models.Deelaspect GetDeelaspectById(int id);
        BeoordelingProject.Models.Matrix GetMatrixByID(int id);
    }
}
