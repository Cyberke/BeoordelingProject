using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BeoordelingProject.ViewModel {
    public class AdminpaneelVM {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public ManageUserViewModel WachtwoordVM { get; set; }
        public bool AutoFeedback { get; set; }
    }
}