using BeoordelingProject.DAL.Services;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        private IStudentService studentService = null;

        public AdminController()
        {

        }

        public AdminController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public ActionResult Index()
        {
            AdminOverzichtVM vm = new AdminOverzichtVM();
            vm.Studenten = studentService.GetStudenten();
            vm.Opleidingen = studentService.GetOpleidingen();

            WebClient c = new WebClient();
            var json = c.DownloadString("http://datatank.gent.be/Gezondheid/Huisartsen.json");
            vm.StudentenString = new HtmlString(json); ;
            //vm.StudentenString = studentService.SerializeObject(vm.Studenten);

            return View(vm);
        }
	}
}