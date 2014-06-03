namespace BeoordelingProject.Migrations
{
    using BeoordelingProject.DAL.Context;
    using BeoordelingProject.Models;
    using ParkingApplicatie.DataAccess.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BeoordelingProject.DAL.Context.BeoordelingsContext>
    {
        List<Rol> rollen;
        List<Student> studenten;
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BeoordelingProject.DAL.Context.BeoordelingsContext context)
        {
            rollen = new List<Rol>{
              new Rol { Naam = "Promotor"},
              new Rol { Naam = "Tweede lezer"},
              new Rol { Naam = "Kritische vriend"}
            };

            studenten = new List<Student>{
                new Student{Naam="Jelle", Opleiding="NMCT", Email="jelle@mail.be", Geboortedatum="20/08/1993",Geslacht='M', StudentId=245, Trajecttype="IOT"}
            };

            studenten.ForEach(s => context.Studenten.AddOrUpdate(p => p.Naam, s));
            context.SaveChanges();

            rollen.ForEach(r => context.Rollen.AddOrUpdate(p => p.Naam, r));
            context.SaveChanges();

            AddUserAndRoles(context);
            context.SaveChanges();
            
        }

        
        void AddUserAndRoles(BeoordelingsContext context)
        {
            var idManager = new IdentityManagerRepository(context);
            //pas op: paswoord moet voldoen aan de eisen van ASP.NET Passwords

            idManager.CreateRole("Admin", "Global Access");
            idManager.CreateRole("User", "Gewone Gebruiker");

            List<Rol> userRollen = new List<Rol> { rollen[0], rollen[1] };

            var studentrol = new List<StudentRollen>
            {
                new StudentRollen{ Student = studenten[0], Rollen = userRollen}
            };

            var adminUser = new ApplicationUser()
            {
                UserName = "PeterVdK",
                StudentRollen = studentrol
            };

            /*
            var adminUser = new ApplicationUser()
            {
                UserName = "petervdk",
                Studenten = studenten,
                Rollen = userRollen
            };
            var user = new ApplicationUser()
            {
                UserName = "jellevdb",
                Studenten = studenten,
                Rollen = userRollen
            };
            */
            idManager.Create(adminUser, "Password1");
            //idManager.Create(user, "Password1");

            idManager.AddUserToRole(adminUser.Id, "Admin");
            //idManager.AddUserToRole(user.Id, "User");

        }
        
    }
}
