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
    public class BeoordelaarController : Controller
    {
        private IBeoordelingsService beoordelingsService = null;
        //private Beoordeling beoordeling = new Beoordeling();

        public BeoordelaarController(IBeoordelingsService beoordelingsService) {
            this.beoordelingsService = beoordelingsService;
        }

        //
        // GET: /Beoordelaar/
        public ActionResult Index()
        {
            

            return View();
        }

        [HttpPost]
        public ActionResult Index(BeoordelingsVM beoordelingsVM) {
            

            return View(beoordelingsVM);
        }
	}
}