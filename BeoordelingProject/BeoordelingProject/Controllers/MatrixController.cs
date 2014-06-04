using BeoordelingProject.DAL.Services;
using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class MatrixController : Controller
    {
        private IMatrixService matrixService = null;

        public MatrixController()
        {

        }
        public MatrixController(IMatrixService matrixService)
        {
            this.matrixService = matrixService;
        }
        public ActionResult Index()
        {
            Matrix m = matrixService.GetMatrixByID(2);

            return View(m);
        }
	}
}