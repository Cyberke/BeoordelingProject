using BeoordelingProject.DAL.Services;
using BeoordelingProject.Models;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        private IStudentService studentService = null;
        private IAdministratorService adminService = null;

        public AdminController()
        {

        }

        public AdminController(IStudentService studentService, IAdministratorService adminService)
        {
            this.studentService = studentService;
            this.adminService = adminService;
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                AdminOverzichtVM vm = new AdminOverzichtVM();
                vm.Studenten = studentService.GetStudenten();
                vm.Opleidingen = studentService.GetOpleidingen();
                vm.Resultaten = studentService.GetResultaat();
                vm.StudentenString = studentService.MaakStudentString(vm.Studenten, vm.Resultaten);

                return View(vm);
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Beoordelaar");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
	}
}