using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.ViewModel
{
    public class CSVDataVM
    {
        [Required]
        public string csvData { get; set; }

        [Required]
        public string academiejaar { get; set; }
    }
}