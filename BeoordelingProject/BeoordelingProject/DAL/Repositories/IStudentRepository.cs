using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IStudentRepository:IGenericRepository<Student>
    {
        int AantalStudentenEind(string opleiding, int minimum, int maximum);
        int AantalStudentenTussentijds(string opleiding, int minimum, int maximum);
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Student> All();
        System.Collections.Generic.IEnumerable<string> GetOpleidingen();
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Rol> GetRoles();
    }
}
