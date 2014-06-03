using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class Resultaat
    {
        public int ID { get; set; }
        public int StudentId { get; set; }
        public int TussentijdseId { get; set; }
        public double TotaalTussentijdResultaat { get; set; }
        public int EindId { get; set; }
        public double TotaalEindresultaat { get; set; }
        public virtual List<DeelaspectResultaat> DeelaspectResultaten { get; set; }
        public virtual List<HoofdaspectResultaat> HoofdaspectResultaten { get; set; }

    }
}