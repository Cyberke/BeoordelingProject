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
            selectedRollen.Add(studentService.GetRolById(model.SelectedRolId));
            Student selectedStudent = studentService.GetStudentByID(model.SelectedStudentId);
            StudentRollen studentrol = studentrolService.CreateStudentrol(selectedStudent, selectedRollen);

            var user = new ApplicationUser() { UserName = model.Account.UserName };
            List<StudentRollen> studentrollen = new List<StudentRollen>();
            studentrollen.Add(studentrol);
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