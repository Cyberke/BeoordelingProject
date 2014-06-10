using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class Deelaspect
    {
        public int ID { get; set; }
        public int Weging { get; set; }
        public string Omschrijving { get; set; }
        public string VOVOmschrijving { get; set; }
        public string OVOmschrijving { get; set; }
        public string VOmschrijving { get; set; }
        public string RVOmschrijving { get; set; }
        public string GOmschrijving { get; set; }
        public string ZGOmschrijving { get; set; }
        public virtual List<Rol> Rollen { get; set; }
    }
}