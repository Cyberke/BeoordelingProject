using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IStudentService
    {
        System.Collections.Generic.List<BeoordelingProject.Models.Student> CreateStudenten(string csvData, string academiejaar);
        void DeleteUser(BeoordelingProject.Models.ApplicationUser user);
        int GetAantalByOpleiding(string opleiding);
        int GetAantalEind(string opleiding, int minimum, int maximum);
        int GetAantalTeTonenStudenten(System.Collections.Generic.List<BeoordelingProject.Models.StudentRollen> studentRollen);
        int GetAantalTussentijds(string opleiding, int minimum, int maximum);
        System.Collections.Generic.List<string> GetOpleidingen();
        System.Collections.Generic.List<BeoordelingProject.Models.Resultaat> GetResultaat();
        BeoordelingProject.Models.Resultaat GetResultaatByStudentId(int id);
        BeoordelingProject.Models.Rol GetRolById(int id);
        System.Collections.Generic.List<BeoordelingProject.Models.Rol> GetRoles();
        System.Collections.Generic.List<System.Collections.Generic.List<BeoordelingProject.Models.Rol>> GetRollenByStudent(System.Collections.Generic.List<BeoordelingProject.Models.StudentRollen> studentRollen);
        BeoordelingProject.Models.Student GetStudentByID(int id);
        System.Collections.Generic.List<BeoordelingProject.Models.Student> GetStudenten();
        System.Collections.Generic.List<BeoordelingProject.Models.Student> GetStudentenByStudentRollen(System.Collections.Generic.List<BeoordelingProject.Models.StudentRollen> studentRollen);
        BeoordelingProject.Models.Student GetUserById(int Id);
        BeoordelingProject.Models.ApplicationUser GetUserById(string userId);
        System.Collections.Generic.List<BeoordelingProject.Models.ApplicationUser> GetUsers();
        System.Web.IHtmlString MaakStudentString(object studentlijst, object resultaatlijst);
        System.Web.IHtmlString SerializeObject(object beoordelaarslijst);
        System.Web.IHtmlString SerializeObject(object studentenlijst, object studentPerRollenlijst, object userID);
    }
}
