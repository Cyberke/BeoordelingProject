using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public string Trajecttype { get; set; }
        public string Opleiding { get; set; }
        public string Email { get; set; }
        public int StudentId { get; set; }
        public string Geslacht { get; set; }
        public string Geboortedatum { get; set; }
        public string academiejaar { get; set; }
    }
}