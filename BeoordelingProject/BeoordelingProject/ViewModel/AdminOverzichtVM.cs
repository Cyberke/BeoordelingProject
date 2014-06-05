using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.ViewModel
{
    public class AdminOverzichtVM
    {
        public AdminOverzichtVM()
        {

        }

        public List<String> Opleidingen { get; set; }
        public List<Student> Studenten { get; set; }

        public IHtmlString StudentenString { get; set; }
    }
}