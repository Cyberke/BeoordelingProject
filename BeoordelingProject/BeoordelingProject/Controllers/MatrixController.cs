using BeoordelingProject.DAL.Services;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class MatrixController : Controller
    {
        private MatrixbeheerService matrixbeheerservice = null;
        public MatrixController()
        {

        }
        public MatrixController(MatrixbeheerService matrixbeheerservice)
        {
            this.matrixbeheerservice = matrixbeheerservice;
        }
        //
        // GET: /Matrix/
        public ActionResult EditMatrix(string opleiding, bool tussentijds = false)
        {
            if (opleiding != null && !opleiding.Equals("") )
            {
                MatrixbeheerVM vm = new MatrixbeheerVM();
                vm.Matrix = matrixbeheerservice.GetMatrixByRichtingByTussentijds(opleiding, tussentijds);

                if (vm.Matrix != null)
                {
                    vm.Rollen = matrixbeheerservice.getRollenMatrix(vm.Matrix.ID);
                    vm.hoofdaspecten = matrixbeheerservice.GetHoofdaspectenByMatrixId(vm.Matrix.ID);
                    return View(vm);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditMatrix(MatrixbeheerVM vm)
        {

            //matrixbeheerservice.UpdateMatrix(vm.Matrix);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            MatrixbeheerVM vm = new MatrixbeheerVM();
            vm.Opleidingen = matrixbeheerservice.GetOpleidingen();
            return View(vm);
        }
	}
}