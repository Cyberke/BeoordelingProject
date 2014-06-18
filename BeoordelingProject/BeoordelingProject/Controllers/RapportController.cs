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
        //
        // GET: /Rapport/
        public ActionResult Index()
        {
            RapportVM rapport = new RapportVM
            {
                Academiejaar = "2014-2015",
                Naam = "Peter VandenKerckhove",
                Richting = "BaKo",
                Punt = 18
            };

            return new RazorPDF.PdfResult(rapport, "Index");
        }
	}
}