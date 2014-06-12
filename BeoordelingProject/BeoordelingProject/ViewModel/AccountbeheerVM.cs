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
        public AccountbeheerVM()
        {

        }

        public List<Student> Studenten { get; set; }
        public List<int> SelectedStudentId { get; set; }
        public List<ApplicationUser> Accounts { get; set; }
        public string SelectedAccountId { get; set; }
        public ApplicationUser Account { get; set; }
        public List<Rol> Rollen { get; set; }
        public List<int> SelectedRolId { get; set; }
    }
}