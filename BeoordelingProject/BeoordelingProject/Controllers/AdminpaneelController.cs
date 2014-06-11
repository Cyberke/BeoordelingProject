using BeoordelingProject.DAL.Services;
using BeoordelingProject.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class AdminpaneelController : Controller
    {
        IAdministratorService adminService = null;

        public AdminpaneelController(IAdministratorService adminService) {
            this.adminService = adminService;
        }

        //
        // GET: /Adminpaneel/
        public ActionResult Index()
        {
            AdminpaneelVM vm = new AdminpaneelVM();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(AdminpaneelVM vm, string adminpaneelButtons) {
            if (adminpaneelButtons.Equals("Opslaan")) {
                var email = vm.Email;
                var wachtwoord = vm.WachtwoordVM.NewPassword;
                var autoFeedback = vm.AutoFeedback;

                PasswordHasher pwdHasher = new PasswordHasher();

                var admin = adminService.GetAdminById(User.Identity.GetUserId());
                admin.UserName = email;
                admin.PasswordHash = pwdHasher.HashPassword(wachtwoord);

                adminService.UpdateAdmin(admin, autoFeedback);
            }
            else if (adminpaneelButtons.Equals("Studenten importeren")) {
                return RedirectToAction("Index", "Student");
            }
            else {
                return RedirectToAction("AddStudentRol", "Accountbeheer");
            }

            return RedirectToAction("Index");
        }
	}
}