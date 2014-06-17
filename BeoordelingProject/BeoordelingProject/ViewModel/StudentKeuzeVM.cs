using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.ViewModel {
    public class StudentKeuzeVM {
        public List<Student> Studenten { get; set; }
        public List<List<Rol>> RollenPerStudent { get; set; }
        public int Aantal { get; set; }
        public IHtmlString StudentenString { get; set; }
        public bool CFaanwezig { get; set; }
    }
}