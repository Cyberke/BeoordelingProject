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

        public BeoordelaarController(IBeoordelingsService beoordelingsService, IMatrixService matrixService, IBeoordelingsEngine beoordelingsEngine, IStudentService studentService) {
            this.beoordelingsService = beoordelingsService;
            this.matrixService = matrixService;
            this.beoordelingsEngine = beoordelingsEngine;
            this.studentService = studentService;
        }

        //
        // GET: /Beoordelaar/
        [HttpGet]
        public ActionResult Index()
        {
            BeoordelingsVM vm = new BeoordelingsVM();
            
            vm.Matrix = beoordelingsService.GetMatrixForRol(2, 1);
            vm.Student = studentService.GetStudentByID(1);
            vm.Rol_ID = 2;
            vm.Resultaten = new Resultaat();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(BeoordelingsVM vm)
        {
            Matrix m = matrixService.GetMatrixByID(vm.Matrix.ID);
            Resultaat newres = new Resultaat();
            newres.StudentId = vm.Student.ID;

            if (m.Tussentijds == true)
            {
                newres.TussentijdseId = m.ID;
                newres.DeelaspectResultaten = beoordelingsService.FillDeelaspectResultaten(m, vm.Resultaten.DeelaspectResultaten);

                List<double> scores = beoordelingsService.GetListScore(newres.DeelaspectResultaten);
                List<int> wegingen = beoordelingsService.GetListWegingen(newres.DeelaspectResultaten);

                newres.TotaalTussentijdResultaat = beoordelingsEngine.totaalScore(scores, wegingen);

                
            }
            else
            {
                //eindscoreberekening
            }
            return View();
        }
	}
}