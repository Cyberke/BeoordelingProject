using BeoordelingProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeoordelingProject.ViewModel
{
    public class AccountbeheerVM
    {
        public AccountbeheerVM() {

        }

        public List<SelectListItem> Studenten { get; set; }
        public int[] SelectedStudent { get; set; }
        public List<SelectListItem> Accounts { get; set; }
        public int[] SelectedAccount { get; set; }
        public ApplicationUser Account { get; set; }
        public int[] SelectedRol { get; set; }
        public List<Rol> Rollen { get; set; }
    }
}