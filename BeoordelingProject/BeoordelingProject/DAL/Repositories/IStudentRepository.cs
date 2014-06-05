using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
<<<<<<< HEAD
        //System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Student> All();
=======
>>>>>>> f57474618b1183572524b1f3211053a672009fa0
        System.Collections.Generic.IEnumerable<string> GetOpleidingen();
    }
}
