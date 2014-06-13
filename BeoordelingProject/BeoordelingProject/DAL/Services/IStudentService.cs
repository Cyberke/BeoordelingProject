using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IStudentService
    {
        System.Collections.Generic.List<BeoordelingProject.Models.Student> CreateStudenten(string csvData);
        void DeleteUser(BeoordelingProject.Models.ApplicationUser user);
        int GetAantalTeTonenStudenten(System.Collections.Generic.List<BeoordelingProject.Models.StudentRollen> studentRollen);
        System.Collections.Generic.List<string> GetOpleidingen();
        BeoordelingProject.Models.Rol GetRolById(int id);
        System.Collections.Generic.List<BeoordelingProject.Models.Rol> GetRoles();
        System.Collections.Generic.List<System.Collections.Generic.List<BeoordelingProject.Models.Rol>> GetRollenByStudent(System.Collections.Generic.List<BeoordelingProject.Models.StudentRollen> studentRollen);
        BeoordelingProject.Models.Student GetStudentByID(int id);
        System.Collections.Generic.List<BeoordelingProject.Models.Student> GetStudenten();
        System.Collections.Generic.List<BeoordelingProject.Models.Student> GetStudentenByStudentRollen(System.Collections.Generic.List<BeoordelingProject.Models.StudentRollen> studentRollen);
        BeoordelingProject.Models.ApplicationUser GetUserById(string userId);
        System.Collections.Generic.List<BeoordelingProject.Models.ApplicationUser> GetUsers();
        System.Web.IHtmlString SerializeObject(object value);
        System.Web.IHtmlString SerializeObject(object value, object otherValue);
    }
}
