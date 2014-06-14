using BeoordelingProject.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BeoordelingProject.DAL.Context
{
    public class BeoordelingsContext: IdentityDbContext<ApplicationUser>
    {
        public BeoordelingsContext()
            :base("BeoordelingProject")
        {

        }

        public DbSet<Student> Studenten { get; set; }
        public DbSet<Rol> Rollen { get; set; }
        public DbSet<Resultaat> Resultaten { get; set; }
        public DbSet<Hoofdaspect> Hoofdaspecten { get; set; }
        public DbSet<HoofdaspectResultaat> HoofdaspectResultaten { get; set; }
        public DbSet<Deelaspect> Deelaspect { get; set; }
        public DbSet<DeelaspectResultaat> DeelaspectResultaten { get; set; }
        public DbSet<Matrix> Matrices { get; set; }
        public DbSet<StudentRollen> StudentRollen { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<ApplicationUser>().HasMany(a => a.Studenten).WithMany();
            modelBuilder.Entity<ApplicationUser>().HasMany(s => s.Rollen).WithMany();
            */

            modelBuilder.Entity<ApplicationUser>().HasMany(a => a.StudentRollen).WithMany();

            modelBuilder.Entity<StudentRollen>().HasMany(s => s.Rollen).WithMany();

            modelBuilder.Entity<Hoofdaspect>().HasMany(h => h.Deelaspecten).WithMany();
            modelBuilder.Entity<Matrix>().HasMany(m => m.Hoofdaspecten).WithMany();

            modelBuilder.Entity<Resultaat>().HasMany(r => r.DeelaspectResultaten).WithMany();
            modelBuilder.Entity<Resultaat>().HasMany(r => r.HoofdaspectResultaten).WithMany();

            modelBuilder.Entity<Hoofdaspect>().HasMany(h => h.Rollen).WithMany();

            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            // Keep this:
            modelBuilder.Entity<IdentityUser>().ToTable("AspNetUsers");

            // Change TUser to ApplicationUser everywhere else - 
            // IdentityUser and ApplicationUser essentially 'share' the AspNetUsers Table in the database:
            EntityTypeConfiguration<ApplicationUser> table =
                modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");

            table.Property((ApplicationUser u) => u.UserName).IsRequired();

            // EF won't let us swap out IdentityUserRole for ApplicationUserRole here:
            modelBuilder.Entity<ApplicationUser>().HasMany<IdentityUserRole>((ApplicationUser u) => u.Roles);
            modelBuilder.Entity<IdentityUserRole>().HasKey((IdentityUserRole r) =>
                new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("AspNetUserRoles");

            // Leave this alone:
            EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
                modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
                    new
                    {
                        UserId = l.UserId,
                        LoginProvider = l.LoginProvider,
                        ProviderKey
                            = l.ProviderKey
                    }).ToTable("AspNetUserLogins");

            entityTypeConfiguration.HasRequired<IdentityUser>((IdentityUserLogin u) => u.User);
            EntityTypeConfiguration<IdentityUserClaim> table1 =
                modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");

            table1.HasRequired<IdentityUser>((IdentityUserClaim u) => u.User);

            // Add this, so that IdentityRole can share a table with ApplicationRole:
            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");

            // Change these from IdentityRole to ApplicationRole:
            EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 =
                modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles");

            entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();

        }

    }
}