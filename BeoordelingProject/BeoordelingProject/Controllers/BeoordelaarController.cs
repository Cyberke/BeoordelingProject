using BeoordelingProject.DAL.Services;
using BeoordelingProject.Engine;
using BeoordelingProject.Models;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.IO;
using System.Net.Mail;

namespace BeoordelingProject.Controllers
{
    public class BeoordelaarController : Controller
    {
        private IBeoordelingsService beoordelingsService = null;
        private IBeoordelingsEngine beoordelingsEngine = null;
        private IStudentService studentService = null;
        private IUserManagementService userService = null;
        private IAdministratorService adminService = null;

        public BeoordelaarController(
            IBeoordelingsService beoordelingsService,
            IBeoordelingsEngine beoordelingsEngine,
            IStudentService studentService,
            IUserManagementService userService,
            IAdministratorService adminService) {
            this.beoordelingsService = beoordelingsService;
            this.beoordelingsEngine = beoordelingsEngine;
            this.studentService = studentService;
            this.userService = userService;
            this.adminService = adminService;
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
            int count = beoordelingsService.getTotaalAantalDeelaspecten(vm.MatrixID, vm.Rol_ID);

            if(vm.Scores != null)
            {
                if (count == vm.Scores.Count)
                {
                    vm.Matrix = beoordelingsService.GetMatrix(vm.MatrixID);
                    beoordelingsService.CreateBeoordeling(vm);

                    ApplicationUser admin = adminService.GetAdmin();
                    if (admin.MailZenden)
                    {
                        beoordelingsService.stuurMail(vm.Student.ID);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    vm.Scores.Clear();
                }
                
            }

            vm.Matrix = beoordelingsService.GetMatrixForRol(vm.MatrixID, vm.Rol_ID);

            vm.Scores = new List<double>();
            for (int i = 0; i < count; i++ )
            {
                vm.Scores.Add(0);
            }
<<<<<<< HEAD
=======
            
            ViewBag.Error = "Gelieve een graad voor ieder deelaspect in te vullen";
            vm.Student = studentService.GetStudentByID(vm.Student.ID);
            vm.Resultaten = new Resultaat();
            return View(vm);
>>>>>>> bd66ea047a2395021e015c7aac879677edb2d027
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

        public ActionResult Rapport(int id)
        {
            Student student = studentService.GetStudentByID(id);
            foreach(Resultaat resultaat in studentService.GetResultaat())
            {
                if(resultaat.StudentId == id)
                {
                    double punt = studentService.GetResultaatByStudentId(id).TotaalEindresultaat;
                    if (punt > -1)
                    {
                        RapportVM rapport = new RapportVM
                        {
                            Academiejaar = student.academiejaar,
                            Naam = student.Naam,
                            Richting = student.Opleiding,
                            Punt = studentService.GetResultaatByStudentId(id).TotaalEindresultaat,
                            Feedback = studentService.GetResultaatByStudentId(id).CustomFeedback
                        };
                        return new RazorPDF.PdfResult(rapport, "Rapport");
                    }
                    else
                        return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("Index", "Admin"); 
        }
	}
}
