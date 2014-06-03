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

            var adminUser = new ApplicationUser()
            {
                UserName = "ddpadmin",
                Gebruikersnaam = "ddpadmin",
                Rollen = new List<Rol>
                {
                    new Rol{Naam="Promotor"},
                    new Rol{Naam="Tweede Lezer"}
                }
            };

            var user = new ApplicationUser()
            {
                UserName = "johanuser",
                Gebruikersnaam = "johanuser",
                Rollen = new List<Rol>
                {
                    new Rol{Naam="Tweede Lezer"},
                    new Rol{Naam="Kritische vriend"}
                }, 
            };

            idManager.Create(adminUser, "Password1");
            idManager.Create(user, "Password1");

            idManager.AddUserToRole(adminUser.Id, "Admin");
            idManager.AddUserToRole(user.Id, "User");

        }
    }
}
