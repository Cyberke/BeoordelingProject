using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IResultaatRepository : IGenericRepository<Resultaat>
    {
        BeoordelingProject.Models.Resultaat getByStudentId(int studentid);
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Resultaat> GetEindResultaten(int id);
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Resultaat> GetTussentijdseResultaten(int id);
        int ifExistsGetStudentId(int studentid);
    }
}
