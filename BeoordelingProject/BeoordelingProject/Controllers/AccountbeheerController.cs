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
        
        public AccountbeheerController()
        {

        }

        public AccountbeheerController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        public ActionResult AddStudentRol()
        {
            var accountbeheerVM = new AccountbeheerVM();
            accountbeheerVM.Studenten = GetStudenten();
            accountbeheerVM.Accounts = GetUsers();
            return View(accountbeheerVM);
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