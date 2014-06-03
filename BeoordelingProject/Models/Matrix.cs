using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class Matrix
    {
        public int ID { get; set; }
        public string Richting { get; set; }
        public bool Tussentijds { get; set; }
        public virtual List<Hoofdaspect> Hoofdaspecten { get; set; }
    }
}