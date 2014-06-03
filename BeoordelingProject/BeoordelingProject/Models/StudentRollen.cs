using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class StudentRollen
    {
        [Key]
        public int Rol_ID { get; set; }
        public virtual Student Student { get; set; }
        public virtual List<Rol> Rollen { get; set; }
    }
}