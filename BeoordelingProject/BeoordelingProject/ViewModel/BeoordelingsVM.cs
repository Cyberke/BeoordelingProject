using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.ViewModel {
    public class BeoordelingsVM {
        public List<Resultaat> Resultaten { get; set; }
        public string Graad { get; set; }
        public Matrix Matrix { get; set; }
    }
}