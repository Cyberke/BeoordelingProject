using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class DeelaspectResultaat
    {
        public int ID { get; set; }
        public int DeelaspectId { get; set; }
        public double Score { get; set; }
    }
}