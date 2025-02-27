﻿using BeoordelingProject.Models;
using System;
namespace BeoordelingProject.DAL.Repositories
{
    public interface IResultaatRepository : IGenericRepository<Resultaat>
    {
        System.Collections.Generic.List<string> CheckIfRolesCompleted(int studentid);
        int GetAantalRollenForHoofdaspect(int aspectID, bool cfaanwezig);
        BeoordelingProject.Models.Resultaat getByStudentId(int studentid);
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Resultaat> GetEindResultaten(int id);
        System.Collections.Generic.List<double> GetScoresForHoofdaspect(int aspectID, int studentID);
        System.Collections.Generic.IEnumerable<BeoordelingProject.Models.Resultaat> GetTussentijdseResultaten(int id);
        int ifExistsGetStudentId(int studentid);
        bool isCFaanwezig(int studentid);
    }
}
