using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeoordelingProject.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Gebruikersnaam { get; set; }
        public virtual List<Rol> Rollen { get; set; }
    }
}