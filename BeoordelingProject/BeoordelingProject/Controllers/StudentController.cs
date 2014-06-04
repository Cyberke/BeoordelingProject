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
    public class StudentController : Controller
    {
        //
        // GET: /Student/

        private IStudentService studentService = null;

        public StudentController()
        {

        }

        public StudentController(StudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            CSVDataVM vm = new CSVDataVM();
            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(CSVDataVM vm)
        {
            studentService.CreateStudenten(vm.csvData);
            return RedirectToAction("Index", "Home");
        }


        public ActionResult StudentLijst()
        {
            List<Student> studenten = studentService.GetStudenten();
            return View(studenten);
        }
	}
}