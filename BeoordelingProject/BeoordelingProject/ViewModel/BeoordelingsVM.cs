using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.ViewModel {
    public class BeoordelingsVM {
        public Resultaat Resultaten { get; set; }
        public int MatrixID { get; set; }
        public Matrix Matrix { get; set; }
        public Student Student { get; set; }
        public int Rol_ID { get; set; }
        public List<double> Scores { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string feedback { get; set; }

        public bool CFaanwezig { get; set; }
        public bool breekpunten { get; set; }
    }
}