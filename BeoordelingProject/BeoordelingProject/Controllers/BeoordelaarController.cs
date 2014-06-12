using BeoordelingProject.DAL.Services;
using BeoordelingProject.Engine;
using BeoordelingProject.Models;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class BeoordelaarController : Controller
    {
        private IBeoordelingsService beoordelingsService = null;
        private IMatrixService matrixService = null;
        private IBeoordelingsEngine beoordelingsEngine = null;
        private IStudentService studentService = null;
        private IUserManagementService userService = null;

        public BeoordelaarController(
            IBeoordelingsService beoordelingsService,
            IMatrixService matrixService,
            IBeoordelingsEngine beoordelingsEngine,
            IStudentService studentService,
            IUserManagementService userService) {
            this.beoordelingsService = beoordelingsService;
            this.matrixService = matrixService;
            this.beoordelingsEngine = beoordelingsEngine;
            this.studentService = studentService;
            this.userService = userService;
        }

        //
        // GET: /Beoordelaar/
        [HttpGet]
        public ActionResult Beoordeling()
        {
            BeoordelingsVM vm = new BeoordelingsVM();
            
            vm.Matrix = beoordelingsService.GetMatrixForRol(2, 1);
            vm.Student = studentService.GetStudentByID(1);
            vm.Rol_ID = 2;
            vm.Resultaten = new Resultaat();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Beoordeling(BeoordelingsVM vm)
        {
            return View();
        }

        public ActionResult Index() {
            StudentKeuzeVM vm = new StudentKeuzeVM();

            ApplicationUser beoordelaar = userService.GetUser(User.Identity.Name);

            vm.Studenten = studentService.GetStudentenByStudentRollen(beoordelaar.StudentRollen);
            vm.RollenPerStudent = studentService.GetRollenByStudent(beoordelaar.StudentRollen);
            vm.Aantal = studentService.GetAantalTeTonenStudenten(beoordelaar.StudentRollen);
            vm.StudentenString = studentService.SerializeObject(vm.Studenten, vm.RollenPerStudent);

            return View(vm);
        }
	}
}