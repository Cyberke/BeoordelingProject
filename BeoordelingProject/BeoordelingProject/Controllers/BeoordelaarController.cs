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
        public ActionResult Beoordeling(string studentRol, int matrix = 0)
        {
            if (studentRol != null && !studentRol.Equals("")) {
                string[] parts = studentRol.Split('.');

                int studentID = 0;
                int rolID = 0;

                if (parts.Length == 2) {
                    if (int.TryParse(parts[0], out studentID) && int.TryParse(parts[1], out rolID)) {
                        if (matrix != 0) {
                            BeoordelingsVM vm = new BeoordelingsVM();

                            vm.Matrix = beoordelingsService.GetMatrixForRol(matrix, rolID);
                            vm.Student = studentService.GetStudentByID(studentID);
                            vm.Rol_ID = rolID;
                            vm.Resultaten = new Resultaat();

                            return View(vm);
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Beoordeling(BeoordelingsVM vm)
        {
            beoordelingsService.CreateBeoordeling(vm);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]
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