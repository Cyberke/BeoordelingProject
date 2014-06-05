using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IResultaatRepository : IGenericRepository<Resultaat>
    {
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Resultaat> GetEindResultaten(int id);
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Resultaat> GetTussentijdseResultaten(int id);
    }
}
