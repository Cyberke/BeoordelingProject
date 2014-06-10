using System;
namespace BeoordelingProject.DAL.Services
{
    public interface IStudentService
    {
        System.Collections.Generic.List<BeoordelingProject.Models.Student> CreateStudenten(string csvData);
        void DeleteUser(BeoordelingProject.Models.ApplicationUser user);
        System.Collections.Generic.List<string> GetOpleidingen();
        BeoordelingProject.Models.Rol GetRolById(int id);
        System.Collections.Generic.List<BeoordelingProject.Models.Rol> GetRoles();
        BeoordelingProject.Models.Student GetStudentByID(int id);
        System.Collections.Generic.List<BeoordelingProject.Models.Student> GetStudenten();
        BeoordelingProject.Models.ApplicationUser GetUserById(string id);
        System.Collections.Generic.List<BeoordelingProject.Models.ApplicationUser> GetUsers();
        System.Web.IHtmlString SerializeObject(object value);
    }
}
