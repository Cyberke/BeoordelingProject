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
    public class RapportController : Controller
    {

        private IStudentService studentService = null;

        public RapportController()
        {

        }

        public RapportController(IStudentService studentService)
        {
            this.studentService = studentService;
        }


        public ActionResult Index(int id)
        {
            Student student = studentService.GetStudentByID(id);

            RapportVM rapport = new RapportVM
            {
                Academiejaar = student.academiejaar,
                Naam = student.Naam,
                Richting = student.Opleiding,
                Punt = studentService.GetResultaatByStudentId(id).TotaalEindresultaat
            };
            return new RazorPDF.PdfResult(rapport, "Index");

        }
	}
}