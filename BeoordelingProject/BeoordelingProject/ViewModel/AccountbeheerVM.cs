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

        public SelectList Studenten { get; set; }
        public int SelectedStudentId { get; set; }
        public List<ApplicationUser> Accounts { get; set; }
        public string SelectedAccountId { get; set; }
        public ApplicationUser Account { get; set; }
        public SelectList Rollen { get; set; }
        public int SelectedRolId { get; set; }
    }
}