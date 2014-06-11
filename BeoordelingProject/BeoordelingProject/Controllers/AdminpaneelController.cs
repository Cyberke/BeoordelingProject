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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            AdminpaneelVM vm = new AdminpaneelVM();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(AdminpaneelVM vm, string adminpaneelButtons) {
            if (ModelState.IsValid) {
                var email = vm.Email;
                var wachtwoord = vm.WachtwoordVM.NewPassword;
                var autoFeedback = vm.AutoFeedback;

                PasswordHasher pwdHasher = new PasswordHasher();

                var admin = adminService.GetAdminById(User.Identity.GetUserId());

                if (pwdHasher.VerifyHashedPassword(admin.PasswordHash, vm.WachtwoordVM.OldPassword) == PasswordVerificationResult.Success) {
                    admin.UserName = email;
                    admin.PasswordHash = pwdHasher.HashPassword(wachtwoord);

                    adminService.UpdateAdmin(admin, autoFeedback);

                    ViewBag.FeedBack = "Wachtwoord veranderd!";

                    return View(new AdminpaneelVM());
                }
                else {
                    ViewBag.FeedBack = "Wachtwoord niet veranderd omdat huidige wachtwoord niet klopt";
                }
            }

            return View(vm);
        }
	}
}