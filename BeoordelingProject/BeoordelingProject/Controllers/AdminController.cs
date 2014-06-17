using BeoordelingProject.DAL.Services;
using BeoordelingProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
            if (User.IsInRole("Admin"))
            {
                AdminOverzichtVM vm = new AdminOverzichtVM();
                vm.Studenten = studentService.GetStudenten();
                vm.Opleidingen = studentService.GetOpleidingen();
                vm.Resultaten = studentService.GetResultaat();
                vm.StudentenString = studentService.MaakStudentString(vm.Studenten, vm.Resultaten);

                return View(vm);
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToAction("Index", "Beoordelaar");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult SendMail(int id)
        {
            var pdf = new Rotativa.ActionAsPdf("GetStudent", new { id = id });
            DateTime now = DateTime.Now;
            string dat = now.Day + "_" + now.Month + "_" + now.Year;
 
            string filepath = Server.MapPath("~/rapport/");
            var file = String.Format(filepath + "rapport_{0}_{1}.pdf",id, dat);
            var binary = pdf.BuildPdf(this.ControllerContext);

            bool isExcists = System.IO.Directory.Exists(file);
            if (!isExcists)
                System.IO.Directory.CreateDirectory(filepath);

            System.IO.File.WriteAllBytes(file, binary);
            


            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("jelle.vanden.bulcke@student.howest.be");
            msg.To.Add("jelle.vanden.bulcke@student.howest.be");
            string bodyTekst = "Hier is het rapport van x\n";
            msg.Body = bodyTekst;
            msg.Subject = "BP Rapport van student x";
            //waarschijnlijk nog aanpassen
            msg.Priority = MailPriority.Normal;

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(msg.From.Address, "raika123");
            client.Host = "smtp.office365.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            client.EnableSsl = true;


            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this e-mail message.
            msg.Attachments.Add(data);         


            client.Send(msg);
            return RedirectToAction("Index");
        }
	}
}