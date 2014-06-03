namespace BeoordelingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deelaspects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Weging = c.Int(nullable: false),
                        Omschrijving = c.String(),
                        VOVOmschrijving = c.String(),
                        OVOmschrijving = c.String(),
                        VOmschrijving = c.String(),
                        RVOmschrijving = c.String(),
                        GOmschrijving = c.String(),
                        ZGOmschrijving = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeelaspectResultaats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeelaspectId = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Hoofdaspects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Weging = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HoofdaspectResultaats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HoofdaspectId = c.Int(nullable: false),
                        Score = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Matrices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Richting = c.String(),
                        Tussentijds = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Resultaats",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        TussentijdseId = c.Int(nullable: false),
                        TotaalTussentijdResultaat = c.Double(nullable: false),
                        EindId = c.Int(nullable: false),
                        TotaalEindresultaat = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rols",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(),
                        Trajecttype = c.String(),
                        Opleiding = c.String(),
                        Email = c.String(),
                        StudentId = c.Int(nullable: false),
                        Geboortedatum = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Gebruikersnaam = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.HoofdaspectDeelaspects",
                c => new
                    {
                        Hoofdaspect_ID = c.Int(nullable: false),
                        Deelaspect_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Hoofdaspect_ID, t.Deelaspect_ID })
                .ForeignKey("dbo.Hoofdaspects", t => t.Hoofdaspect_ID, cascadeDelete: true)
                .ForeignKey("dbo.Deelaspects", t => t.Deelaspect_ID, cascadeDelete: true)
                .Index(t => t.Hoofdaspect_ID)
                .Index(t => t.Deelaspect_ID);
            
            CreateTable(
                "dbo.MatrixHoofdaspects",
                c => new
                    {
                        Matrix_ID = c.Int(nullable: false),
                        Hoofdaspect_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Matrix_ID, t.Hoofdaspect_ID })
                .ForeignKey("dbo.Matrices", t => t.Matrix_ID, cascadeDelete: true)
                .ForeignKey("dbo.Hoofdaspects", t => t.Hoofdaspect_ID, cascadeDelete: true)
                .Index(t => t.Matrix_ID)
                .Index(t => t.Hoofdaspect_ID);
            
            CreateTable(
                "dbo.ResultaatDeelaspectResultaats",
                c => new
                    {
                        Resultaat_ID = c.Int(nullable: false),
                        DeelaspectResultaat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resultaat_ID, t.DeelaspectResultaat_ID })
                .ForeignKey("dbo.Resultaats", t => t.Resultaat_ID, cascadeDelete: true)
                .ForeignKey("dbo.DeelaspectResultaats", t => t.DeelaspectResultaat_ID, cascadeDelete: true)
                .Index(t => t.Resultaat_ID)
                .Index(t => t.DeelaspectResultaat_ID);
            
            CreateTable(
                "dbo.ResultaatHoofdaspectResultaats",
                c => new
                    {
                        Resultaat_ID = c.Int(nullable: false),
                        HoofdaspectResultaat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resultaat_ID, t.HoofdaspectResultaat_ID })
                .ForeignKey("dbo.Resultaats", t => t.Resultaat_ID, cascadeDelete: true)
                .ForeignKey("dbo.HoofdaspectResultaats", t => t.HoofdaspectResultaat_ID, cascadeDelete: true)
                .Index(t => t.Resultaat_ID)
                .Index(t => t.HoofdaspectResultaat_ID);
            
            CreateTable(
                "dbo.ApplicationUserRols",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Rol_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Rol_ID })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rols", t => t.Rol_ID, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Rol_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserRols", "Rol_ID", "dbo.Rols");
            DropForeignKey("dbo.ApplicationUserRols", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ResultaatHoofdaspectResultaats", "HoofdaspectResultaat_ID", "dbo.HoofdaspectResultaats");
            DropForeignKey("dbo.ResultaatHoofdaspectResultaats", "Resultaat_ID", "dbo.Resultaats");
            DropForeignKey("dbo.ResultaatDeelaspectResultaats", "DeelaspectResultaat_ID", "dbo.DeelaspectResultaats");
            DropForeignKey("dbo.ResultaatDeelaspectResultaats", "Resultaat_ID", "dbo.Resultaats");
            DropForeignKey("dbo.MatrixHoofdaspects", "Hoofdaspect_ID", "dbo.Hoofdaspects");
            DropForeignKey("dbo.MatrixHoofdaspects", "Matrix_ID", "dbo.Matrices");
            DropForeignKey("dbo.HoofdaspectDeelaspects", "Deelaspect_ID", "dbo.Deelaspects");
            DropForeignKey("dbo.HoofdaspectDeelaspects", "Hoofdaspect_ID", "dbo.Hoofdaspects");
            DropIndex("dbo.ApplicationUserRols", new[] { "Rol_ID" });
            DropIndex("dbo.ApplicationUserRols", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.ResultaatHoofdaspectResultaats", new[] { "HoofdaspectResultaat_ID" });
            DropIndex("dbo.ResultaatHoofdaspectResultaats", new[] { "Resultaat_ID" });
            DropIndex("dbo.ResultaatDeelaspectResultaats", new[] { "DeelaspectResultaat_ID" });
            DropIndex("dbo.ResultaatDeelaspectResultaats", new[] { "Resultaat_ID" });
            DropIndex("dbo.MatrixHoofdaspects", new[] { "Hoofdaspect_ID" });
            DropIndex("dbo.MatrixHoofdaspects", new[] { "Matrix_ID" });
            DropIndex("dbo.HoofdaspectDeelaspects", new[] { "Deelaspect_ID" });
            DropIndex("dbo.HoofdaspectDeelaspects", new[] { "Hoofdaspect_ID" });
            DropTable("dbo.ApplicationUserRols");
            DropTable("dbo.ResultaatHoofdaspectResultaats");
            DropTable("dbo.ResultaatDeelaspectResultaats");
            DropTable("dbo.MatrixHoofdaspects");
            DropTable("dbo.HoofdaspectDeelaspects");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Students");
            DropTable("dbo.Rols");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Resultaats");
            DropTable("dbo.Matrices");
            DropTable("dbo.HoofdaspectResultaats");
            DropTable("dbo.Hoofdaspects");
            DropTable("dbo.DeelaspectResultaats");
            DropTable("dbo.Deelaspects");
        }
    }
}
