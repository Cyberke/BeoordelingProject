using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class Hoofdaspect
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public int Weging { get; set; }
        public int GewogenScore { get; set; }
        public virtual List<Deelaspect> Deelaspecten {get;set;}
    }
}