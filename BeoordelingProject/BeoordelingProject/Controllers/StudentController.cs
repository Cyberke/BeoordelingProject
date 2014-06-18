using BeoordelingProject.DAL.Services;
using BeoordelingProject.Models;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.IO;

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

        public StudentController(IStudentService studentService)
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
            studentService.CreateStudenten(vm.csvData, vm.academiejaar);
            return RedirectToAction("StudentLijst", "Student");
        }


        public ActionResult StudentLijst()
        {
            List<Student> studenten = studentService.GetStudenten();
            return View(studenten);
        }

        //BAKO CHART TUSSENTIJDS
        [Authorize(Roles = "Admin")]
        public ActionResult ChartTussentijdsBako()
        {
            //Chart aanmaken
            Chart bakoTussen = new Chart();

            bakoTussen.ChartAreas.Add(new ChartArea());
            bakoTussen.Width = 600;
            bakoTussen.Height = 600;
            bakoTussen.Series.Add(new Series("Data"));
            bakoTussen.Series["Data"].ChartType = SeriesChartType.Pie;

            //Punten definiëren
            int ptVovBako = bakoTussen.Series["Data"].Points.AddXY("VOV", studentService.GetAantalTussentijds("BaKo", 0, 7));
            DataPoint vovBako = bakoTussen.Series["Data"].Points[ptVovBako];
            int ptOvBako = bakoTussen.Series["Data"].Points.AddXY("OV", studentService.GetAantalTussentijds("BaKo", 7, 10));
            DataPoint ovBako = bakoTussen.Series["Data"].Points[ptOvBako];
            int ptVBako = bakoTussen.Series["Data"].Points.AddXY("V", studentService.GetAantalTussentijds("BaKo", 10, 12));
            DataPoint vBako = bakoTussen.Series["Data"].Points[ptVBako];
            int ptRvBako = bakoTussen.Series["Data"].Points.AddXY("RV", studentService.GetAantalTussentijds("BaKo", 12, 14));
            DataPoint rvBako = bakoTussen.Series["Data"].Points[ptRvBako];
            int ptGBako = bakoTussen.Series["Data"].Points.AddXY("G", studentService.GetAantalTussentijds("BaKo", 14, 16));
            DataPoint gBako = bakoTussen.Series["Data"].Points[ptGBako];
            int ptZgBako = bakoTussen.Series["Data"].Points.AddXY("ZG", studentService.GetAantalTussentijds("BaKo", 16, 21));
            DataPoint zgBako = bakoTussen.Series["Data"].Points[ptZgBako];

            //Labels definiëren
            bakoTussen.Series["Data"].Label = "#VALY" + " (" + "#PERCENT{P0}" + ")" + "\n #VALX";
            bakoTussen.Series["Data"].LabelForeColor = Color.White;
            bakoTussen.BackColor = Color.Transparent;
            bakoTussen.ChartAreas[0].BackColor = Color.Transparent;
            bakoTussen.BackColor = Color.FromArgb(0,252, 252, 252);
            bakoTussen.Series["Data"].Font = new Font("Segoe UI", 16, FontStyle.Bold);
            bakoTussen.Series["Data"]["PieLabelStyle"] = "Inside";

            //Tonen chart
            var returnStreamBako = new MemoryStream();

            bakoTussen.ImageType = ChartImageType.Png;
            bakoTussen.SaveImage(returnStreamBako);

            returnStreamBako.Position = 0;
             
            return new FileStreamResult(returnStreamBako, "image/png");
        }

        //BAKO CHART EIND
        [Authorize(Roles = "Admin")]
        public ActionResult ChartEindBako()
        {
            //Chart aanmaken
            Chart bakoEind = new Chart();

            bakoEind.ChartAreas.Add(new ChartArea());
            bakoEind.Width = 600;
            bakoEind.Height = 600;
            bakoEind.Series.Add(new Series("Data"));
            bakoEind.Series["Data"].ChartType = SeriesChartType.Pie;

            //Punten definiëren
            int ptVovBako = bakoEind.Series["Data"].Points.AddXY("VOV", studentService.GetAantalEind("BaKo", 0, 7));
            DataPoint vovBako = bakoEind.Series["Data"].Points[ptVovBako];
            int ptOvBako = bakoEind.Series["Data"].Points.AddXY("OV", studentService.GetAantalEind("BaKo", 7, 10));
            DataPoint ovBako = bakoEind.Series["Data"].Points[ptOvBako];
            int ptVBako = bakoEind.Series["Data"].Points.AddXY("V", studentService.GetAantalEind("BaKo", 10, 12));
            DataPoint vBako = bakoEind.Series["Data"].Points[ptVBako];
            int ptRvBako = bakoEind.Series["Data"].Points.AddXY("RV", studentService.GetAantalEind("BaKo", 12, 14));
            DataPoint rvBako = bakoEind.Series["Data"].Points[ptRvBako];
            int ptGBako = bakoEind.Series["Data"].Points.AddXY("G", studentService.GetAantalEind("BaKo", 14, 16));
            DataPoint gBako = bakoEind.Series["Data"].Points[ptGBako];
            int ptZgBako = bakoEind.Series["Data"].Points.AddXY("ZG", studentService.GetAantalEind("BaKo", 16, 21));
            DataPoint zgBako = bakoEind.Series["Data"].Points[ptZgBako];

            //Labels definiëren
            bakoEind.Series["Data"].Label = "#VALY" + " (" + "#PERCENT{P0}" + ")" + "\n #VALX";
            bakoEind.Series["Data"].LabelForeColor = Color.White;
            bakoEind.BackColor = Color.Transparent;
            bakoEind.ChartAreas[0].BackColor = Color.Transparent;
            bakoEind.BackColor = Color.FromArgb(0, 252, 252, 252);
            bakoEind.Series["Data"].Font = new Font("Segoe UI", 16, FontStyle.Bold);
            bakoEind.Series["Data"]["PieLabelStyle"] = "Inside";

            //Tonen chart
            var returnStreamBako = new MemoryStream();

            bakoEind.ImageType = ChartImageType.Png;
            bakoEind.SaveImage(returnStreamBako);

            returnStreamBako.Position = 0;

            return new FileStreamResult(returnStreamBako, "image/png");
        }

        //BALO CHART TUSSENTIJDS
        [Authorize(Roles = "Admin")]
        public ActionResult ChartTussentijdsBalo()
        {
            //Chart aanmaken
            Chart baloTussen = new Chart();

            baloTussen.ChartAreas.Add(new ChartArea());
            baloTussen.Width = 600;
            baloTussen.Height = 600;
            baloTussen.Series.Add(new Series("Data"));
            baloTussen.Series["Data"].ChartType = SeriesChartType.Pie;

            //Punten definiëren
            int ptVovBalo = baloTussen.Series["Data"].Points.AddXY("VOV", studentService.GetAantalTussentijds("BaLo", 0, 7));
            DataPoint vovBalo = baloTussen.Series["Data"].Points[ptVovBalo];
            int ptOvBalo = baloTussen.Series["Data"].Points.AddXY("OV", studentService.GetAantalTussentijds("BaLo", 7, 10));
            DataPoint ovBalo = baloTussen.Series["Data"].Points[ptOvBalo];
            int ptVBalo = baloTussen.Series["Data"].Points.AddXY("V", studentService.GetAantalTussentijds("BaLo", 10, 12));
            DataPoint vBalo = baloTussen.Series["Data"].Points[ptVBalo];
            int ptRvBalo = baloTussen.Series["Data"].Points.AddXY("RV", studentService.GetAantalTussentijds("BaLo", 12, 14));
            DataPoint rvBalo = baloTussen.Series["Data"].Points[ptRvBalo];
            int ptGBalo = baloTussen.Series["Data"].Points.AddXY("G", studentService.GetAantalTussentijds("BaLo", 14, 16));
            DataPoint gBalo = baloTussen.Series["Data"].Points[ptGBalo];
            int ptZgBalo = baloTussen.Series["Data"].Points.AddXY("ZG", studentService.GetAantalTussentijds("BaLo", 16, 21));
            DataPoint zgBalo = baloTussen.Series["Data"].Points[ptZgBalo];

            //Labels definiëren
            baloTussen.Series["Data"].Label = "#VALY" + " (" + "#PERCENT{P0}" + ")" + "\n #VALX";
            baloTussen.Series["Data"].LabelForeColor = Color.White;
            baloTussen.BackColor = Color.Transparent;
            baloTussen.ChartAreas[0].BackColor = Color.Transparent;
            baloTussen.BackColor = Color.FromArgb(0, 252, 252, 252);
            baloTussen.Series["Data"].Font = new Font("Segoe UI", 16, FontStyle.Bold);
            baloTussen.Series["Data"]["PieLabelStyle"] = "Inside";

            //Tonen chart
            var returnStreamBalo = new MemoryStream();
            baloTussen.ImageType = ChartImageType.Png;
            baloTussen.SaveImage(returnStreamBalo);
            returnStreamBalo.Position = 0;
            return new FileStreamResult(returnStreamBalo, "image/png");
        }

        //BALO CHART EIND
        [Authorize(Roles = "Admin")]
        public ActionResult ChartEindBalo()
        {
            //Chart aanmaken
            Chart baloEind = new Chart();

            baloEind.ChartAreas.Add(new ChartArea());
            baloEind.Width = 600;
            baloEind.Height = 600;
            baloEind.Series.Add(new Series("Data"));
            baloEind.Series["Data"].ChartType = SeriesChartType.Pie;

            //Punten definiëren
            int ptVovBalo = baloEind.Series["Data"].Points.AddXY("VOV", studentService.GetAantalEind("BaLo", 0, 7));
            DataPoint vovBalo = baloEind.Series["Data"].Points[ptVovBalo];
            int ptOvBalo = baloEind.Series["Data"].Points.AddXY("OV", studentService.GetAantalEind("BaLo", 7, 10));
            DataPoint ovBalo = baloEind.Series["Data"].Points[ptOvBalo];
            int ptVBalo = baloEind.Series["Data"].Points.AddXY("V", studentService.GetAantalEind("BaLo", 10, 12));
            DataPoint vBalo = baloEind.Series["Data"].Points[ptVBalo];
            int ptRvBalo = baloEind.Series["Data"].Points.AddXY("RV", studentService.GetAantalEind("BaLo", 12, 14));
            DataPoint rvBalo = baloEind.Series["Data"].Points[ptRvBalo];
            int ptGBalo = baloEind.Series["Data"].Points.AddXY("G", studentService.GetAantalEind("BaLo", 14, 16));
            DataPoint gBalo = baloEind.Series["Data"].Points[ptGBalo];
            int ptZgBalo = baloEind.Series["Data"].Points.AddXY("ZG", studentService.GetAantalEind("BaLo", 16, 21));
            DataPoint zgBalo = baloEind.Series["Data"].Points[ptZgBalo];

            //Labels definiëren
            baloEind.Series["Data"].Label = "#VALY" + " (" + "#PERCENT{P0}" + ")" + "\n #VALX";
            baloEind.Series["Data"].LabelForeColor = Color.White;
            baloEind.BackColor = Color.Transparent;
            baloEind.ChartAreas[0].BackColor = Color.Transparent;
            baloEind.BackColor = Color.FromArgb(0, 252, 252, 252);
            baloEind.Series["Data"].Font = new Font("Segoe UI", 16, FontStyle.Bold);
            baloEind.Series["Data"]["PieLabelStyle"] = "Inside";

            //Tonen chart
            var returnStreamBalo = new MemoryStream();
            baloEind.ImageType = ChartImageType.Png;
            baloEind.SaveImage(returnStreamBalo);
            returnStreamBalo.Position = 0;
            return new FileStreamResult(returnStreamBalo, "image/png");
        }

        //CHART BASO TUSSENTIJDS
        [Authorize(Roles = "Admin")]
        public ActionResult ChartTussentijdsBaso()
        {
            //Chart aanmaken
            Chart basoTussen = new Chart();

            basoTussen.ChartAreas.Add(new ChartArea());
            basoTussen.Width = 600;
            basoTussen.Height = 600;
            basoTussen.Series.Add(new Series("Data"));
            basoTussen.Series["Data"].ChartType = SeriesChartType.Pie;

            //Punten definiëren
            int ptVovBaso = basoTussen.Series["Data"].Points.AddXY("VOV", studentService.GetAantalTussentijds("BaSo", 0, 7));
            DataPoint vovBaso = basoTussen.Series["Data"].Points[ptVovBaso];
            int ptOvBaso = basoTussen.Series["Data"].Points.AddXY("OV", studentService.GetAantalTussentijds("BaSo", 7, 10));
            DataPoint ovBaso = basoTussen.Series["Data"].Points[ptOvBaso];
            int ptVBaso = basoTussen.Series["Data"].Points.AddXY("V", studentService.GetAantalTussentijds("BaSo", 10, 12));
            DataPoint vBaso = basoTussen.Series["Data"].Points[ptVBaso];
            int ptRvBaso = basoTussen.Series["Data"].Points.AddXY("RV", studentService.GetAantalTussentijds("BaSo", 12, 14));
            DataPoint rvBaso = basoTussen.Series["Data"].Points[ptRvBaso];
            int ptGBaso = basoTussen.Series["Data"].Points.AddXY("G", studentService.GetAantalTussentijds("BaSo", 14, 16));
            DataPoint gBaso = basoTussen.Series["Data"].Points[ptGBaso];
            int ptZgBaso = basoTussen.Series["Data"].Points.AddXY("ZG", studentService.GetAantalTussentijds("BaSo", 16, 21));
            DataPoint zgBaso = basoTussen.Series["Data"].Points[ptZgBaso];

            //Labels definiëren
            basoTussen.Series["Data"].Label = "#VALY" + " (" + "#PERCENT{P0}" + ")" + "\n #VALX";
            basoTussen.Series["Data"].LabelForeColor = Color.White;
            basoTussen.BackColor = Color.Transparent;
            basoTussen.ChartAreas[0].BackColor = Color.Transparent;
            basoTussen.BackColor = Color.FromArgb(0, 252, 252, 252);
            basoTussen.Series["Data"].Font = new Font("Segoe UI", 16, FontStyle.Bold);
            basoTussen.Series["Data"]["PieLabelStyle"] = "Inside";

            //Tonen chart
            var returnStreamBaso = new MemoryStream();
            basoTussen.ImageType = ChartImageType.Png;
            basoTussen.SaveImage(returnStreamBaso);
            returnStreamBaso.Position = 0;
            return new FileStreamResult(returnStreamBaso, "image/png");
        }

        //CHART BASO EIND
        [Authorize(Roles = "Admin")]
        public ActionResult ChartEindBaso()
        {
            //Chart aanmaken
            Chart basoEind = new Chart();

            basoEind.ChartAreas.Add(new ChartArea());
            basoEind.Width = 600;
            basoEind.Height = 600;
            basoEind.Series.Add(new Series("Data"));
            basoEind.Series["Data"].ChartType = SeriesChartType.Pie;

            //Punten definiëren
            int ptVovBaso = basoEind.Series["Data"].Points.AddXY("VOV", studentService.GetAantalEind("BaSo", 0, 7));
            DataPoint vovBaso = basoEind.Series["Data"].Points[ptVovBaso];
            int ptOvBaso = basoEind.Series["Data"].Points.AddXY("OV", studentService.GetAantalEind("BaSo", 7, 10));
            DataPoint ovBaso = basoEind.Series["Data"].Points[ptOvBaso];
            int ptVBaso = basoEind.Series["Data"].Points.AddXY("V", studentService.GetAantalEind("BaSo", 10, 12));
            DataPoint vBaso = basoEind.Series["Data"].Points[ptVBaso];
            int ptRvBaso = basoEind.Series["Data"].Points.AddXY("RV", studentService.GetAantalEind("BaSo", 12, 14));
            DataPoint rvBaso = basoEind.Series["Data"].Points[ptRvBaso];
            int ptGBaso = basoEind.Series["Data"].Points.AddXY("G", studentService.GetAantalEind("BaSo", 14, 16));
            DataPoint gBaso = basoEind.Series["Data"].Points[ptGBaso];
            int ptZgBaso = basoEind.Series["Data"].Points.AddXY("ZG", studentService.GetAantalEind("BaSo", 16, 21));
            DataPoint zgBaso = basoEind.Series["Data"].Points[ptZgBaso];

            //Labels definiëren
            basoEind.Series["Data"].Label = "#VALY" + " (" + "#PERCENT{P0}" + ")" + "\n #VALX";
            basoEind.Series["Data"].LabelForeColor = Color.White;
            basoEind.BackColor = Color.Transparent;
            basoEind.ChartAreas[0].BackColor = Color.Transparent;
            basoEind.BackColor = Color.FromArgb(0, 252, 252, 252);
            basoEind.Series["Data"].Font = new Font("Segoe UI", 16, FontStyle.Bold);
            basoEind.Series["Data"]["PieLabelStyle"] = "Inside";

            //Tonen chart
            var returnStreamBaso = new MemoryStream();
            basoEind.ImageType = ChartImageType.Png;
            basoEind.SaveImage(returnStreamBaso);
            returnStreamBaso.Position = 0;
            return new FileStreamResult(returnStreamBaso, "image/png");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Charts()
        {
            return View();
        }
    }
}