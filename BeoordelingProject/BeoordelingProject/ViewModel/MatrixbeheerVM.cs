using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.ViewModel
{
    public class MatrixbeheerVM
    {
        public MatrixbeheerVM()
        {

        }

        public List<string> Opleidingen { get; set; }
        public string opleiding { get; set; }
        public bool tussentijds { get; set; }
        public Matrix Matrix { get; set; }
        public List<int> Rollen { get; set; }
    }
}