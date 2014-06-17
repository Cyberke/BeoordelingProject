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
        private IBeoordelingsEngine beoordelingsEngine = null;
        private IStudentService studentService = null;
        private IUserManagementService userService = null;

        public BeoordelaarController(
            IBeoordelingsService beoordelingsService,
            IBeoordelingsEngine beoordelingsEngine,
            IStudentService studentService,
            IUserManagementService userService) {
            this.beoordelingsService = beoordelingsService;
            this.beoordelingsEngine = beoordelingsEngine;
            this.studentService = studentService;
            this.userService = userService;
        }

        //
        // GET: /Beoordelaar/
        public ActionResult Beoordeling(string studentRol, bool cfaanwezig = false, int matrix = 0)
        {
            if (studentRol != null && !studentRol.Equals("")) {
                string[] parts = studentRol.Split('.');

                int studentID = 0;
                int rolID = 0;

                if (parts.Length == 2) {
                    if (int.TryParse(parts[0], out studentID) && int.TryParse(parts[1], out rolID)) {
                        if (matrix != 0) {
                            if (!((rolID == 2 && matrix == 2) || (rolID == 3 && matrix == 2))) {
                                // Geen tweede lezer of kritische vriend mag tussenbeoordelingen doen

                                BeoordelingsVM vm = new BeoordelingsVM();

                                //tussentijds=2, eind=1
                                if(matrix == 1)
                                {
                                    Student temp = studentService.GetStudentByID(studentID);
                                    vm.MatrixID = beoordelingsService.GetMatrixIdByRichtingByType(false, temp.Opleiding);
                                }
                                else
                                {
                                    Student temp = studentService.GetStudentByID(studentID);
                                    vm.MatrixID = beoordelingsService.GetMatrixIdByRichtingByType(true, temp.Opleiding);
                                }

                                vm.CFaanwezig = cfaanwezig;
                                //vm.MatrixID = matrix;
                                vm.Matrix = beoordelingsService.GetMatrixForRol(vm.MatrixID, rolID);
                                vm.Student = studentService.GetStudentByID(studentID);

                                vm.Rol_ID = rolID;
                                vm.Resultaten = new Resultaat();

                                return View(vm);
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Beoordeling(BeoordelingsVM vm)
        {
            vm.Matrix = beoordelingsService.GetMatrix(vm.MatrixID);
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
            vm.StudentenString = studentService.SerializeObject(vm.Studenten, vm.RollenPerStudent, beoordelaar.Id);

            return View(vm);
        }
	}
}
