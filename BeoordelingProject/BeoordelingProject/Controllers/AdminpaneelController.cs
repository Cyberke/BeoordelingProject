using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class AdminpaneelController : Controller
    {
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

                // Service om email, wachtwoord en auto feedback te wijzigen
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