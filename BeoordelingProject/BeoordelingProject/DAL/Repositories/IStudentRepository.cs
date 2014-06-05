using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        System.Collections.Generic.IEnumerable<string> GetOpleidingen();
    }
}
