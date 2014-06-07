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

        public AccountbeheerController(StudentService studentService, IUserManagementService userService, IStudentrolService studentrolService)
        {
            this.studentService = studentService;
            this.userService = userService;
            this.studentrolService = studentrolService;
        }

        public ActionResult AddStudentRol()
        {
            var accountbeheerVM = new AccountbeheerVM();
            accountbeheerVM.Studenten = new SelectList(studentService.GetStudenten(), "ID", "Naam");
            accountbeheerVM.Accounts = new SelectList(studentService.GetUsers(), "Id", "UserName");
            accountbeheerVM.Rollen = studentService.GetRoles();
            return View(accountbeheerVM);
        }

        [HttpPost]
        public ActionResult AddStudentRol(AccountbeheerVM model, FormCollection collection)
        {
            List<Rol> selectedRollen = new List<Rol>();
            Student selectedStudent = studentService.GetStudentByID(model.SelectedStudentId);
            string[] rollenId = collection.GetValues("rollen");
            foreach (string id in rollenId)
            {
                Rol rol = studentService.GetRolById(Convert.ToInt32(id));
                selectedRollen.Add(rol);
            }

            var studentrol = studentrolService.CreateStudentrol(selectedStudent,selectedRollen);

            var user = new ApplicationUser() { UserName = model.Account.UserName};
            var result = userService.Create(user, model.Account.PasswordHash);
            userService.AddUserToRoleUser(user.Id);
            return RedirectToAction("AddStudentRol", "Accountbeheer");

        }
	}
}