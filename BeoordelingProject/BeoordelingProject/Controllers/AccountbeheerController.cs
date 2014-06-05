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
        
        public AccountbeheerController()
        {

        }

        public AccountbeheerController(StudentService studentService, IUserManagementService userService)
        {
            this.studentService = studentService;
            this.userService = userService;
        }

        public ActionResult AddStudentRol()
        {
            var accountbeheerVM = new AccountbeheerVM();
            accountbeheerVM.Studenten = GetStudenten();
            accountbeheerVM.Accounts = GetUsers();
            return View(accountbeheerVM);
        }

        [HttpPost]
        public ActionResult AddStudentRol(AccountbeheerVM model)
        {
            var user = new ApplicationUser() { UserName = model.Account.UserName};
            var result = userService.Create(user, model.Account.PasswordHash);
            userService.AddUserToRoleUser(user.Id);
            return RedirectToAction("AddStudentRol", "Accountbeheer");

        }
        private List<SelectListItem> GetStudenten()
        {
            var studentenList = new List<SelectListItem>();
            foreach (Student student in studentService.GetStudenten())
            {
                studentenList.Add(new SelectListItem { Value = student.ID.ToString(), Text = student.Naam });
            }
            return studentenList;
        }
        private List<SelectListItem> GetUsers()
        {
            var userList = new List<SelectListItem>();
            foreach (ApplicationUser user in studentService.GetUsers())
            {
                userList.Add(new SelectListItem { Value = user.Id, Text = user.UserName });
            }
            return userList;
        }
	}
}