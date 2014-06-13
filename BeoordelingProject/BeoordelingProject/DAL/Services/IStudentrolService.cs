using System;

namespace BeoordelingProject.DAL.Services
{
    public interface IStudentrolService
    {
        BeoordelingProject.Models.StudentRollen CreateStudentrol(BeoordelingProject.Models.Student student, System.Collections.Generic.List<BeoordelingProject.Models.Rol> rollen);
    }
}
