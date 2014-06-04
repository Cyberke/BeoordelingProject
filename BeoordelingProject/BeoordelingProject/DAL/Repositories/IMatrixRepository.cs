using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IMatrixRepository
    {
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Matrix> GetMatrixByID(int id);
    }
}
