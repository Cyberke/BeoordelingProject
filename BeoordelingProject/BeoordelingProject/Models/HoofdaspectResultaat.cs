using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class HoofdaspectResultaat
    {
        public int ID { get; set; }
        public int HoofdaspectId { get; set; }
        public double Score { get; set; }
    }
}