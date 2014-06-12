using BeoordelingProject.DAL.Services;
using BeoordelingProject.Models;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class AccountbeheerController : Controller
    {
        private IStudentService studentService = null;
        private IUserManagementService userService = null;
        private IStudentrolService studentrolService = null;
        
        public AccountbeheerController()
        {

        }

        public AccountbeheerController(IStudentService studentService, IUserManagementService userService, IStudentrolService studentrolService)
        {
            this.studentService = studentService;
            this.userService = userService;
            this.studentrolService = studentrolService;
        }

        [Authorize(Roles="Admin")]
        public ActionResult AddStudentRol()
        {
            var accountbeheerVM = new AccountbeheerVM();
            accountbeheerVM.Studenten = new SelectList(studentService.GetStudenten(), "ID", "Naam");
            accountbeheerVM.Accounts = studentService.GetUsers();
            accountbeheerVM.Rollen = new SelectList(studentService.GetRoles(), "ID","Naam");
            return View(accountbeheerVM);
        }

        [HttpPost]
        public ActionResult AddStudentRol(AccountbeheerVM model, string accountbeheerbuttons)
        {
            List<Rol> selectedRollen = new List<Rol>();
            List<StudentRollen> studentrollen = new List<StudentRollen>();
            for (int id = 0; id <= model.SelectedRolId.Count;id++ )
            {
                selectedRollen.Add(studentService.GetRolById(id));
                Student selectedStudent = studentService.GetStudentByID(id);
                StudentRollen studentrol = studentrolService.CreateStudentrol(selectedStudent, selectedRollen);
                studentrollen.Add(studentrol);
            }

            var user = new ApplicationUser() { UserName = model.Account.UserName };
            user.StudentRollen = studentrollen;
            var result = userService.Create(user, model.Account.PasswordHash);
            userService.AddUserToRoleUser(user.Id);

            return RedirectToAction("AddStudentRol", "Accountbeheer");
        }

        public ActionResult DeleteUser(string userId)
        {
            
            studentService.DeleteUser(userId);
            
            return RedirectToAction("AddStudentRol", "Accountbeheer");
        }
	}
}