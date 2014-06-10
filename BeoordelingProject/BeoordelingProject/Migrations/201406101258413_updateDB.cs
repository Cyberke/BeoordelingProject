namespace BeoordelingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeelaspectRols", "Deelaspect_ID", "dbo.Deelaspects");
            DropForeignKey("dbo.DeelaspectRols", "Rol_ID", "dbo.Rols");
            DropIndex("dbo.DeelaspectRols", new[] { "Deelaspect_ID" });
            DropIndex("dbo.DeelaspectRols", new[] { "Rol_ID" });
            CreateTable(
                "dbo.HoofdaspectRols",
                c => new
                    {
                        Hoofdaspect_ID = c.Int(nullable: false),
                        Rol_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Hoofdaspect_ID, t.Rol_ID })
                .ForeignKey("dbo.Hoofdaspects", t => t.Hoofdaspect_ID, cascadeDelete: true)
                .ForeignKey("dbo.Rols", t => t.Rol_ID, cascadeDelete: true)
                .Index(t => t.Hoofdaspect_ID)
                .Index(t => t.Rol_ID);
            
            DropTable("dbo.DeelaspectRols");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DeelaspectRols",
                c => new
                    {
                        Deelaspect_ID = c.Int(nullable: false),
                        Rol_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Deelaspect_ID, t.Rol_ID });
            
            DropForeignKey("dbo.HoofdaspectRols", "Rol_ID", "dbo.Rols");
            DropForeignKey("dbo.HoofdaspectRols", "Hoofdaspect_ID", "dbo.Hoofdaspects");
            DropIndex("dbo.HoofdaspectRols", new[] { "Rol_ID" });
            DropIndex("dbo.HoofdaspectRols", new[] { "Hoofdaspect_ID" });
            DropTable("dbo.HoofdaspectRols");
            CreateIndex("dbo.DeelaspectRols", "Rol_ID");
            CreateIndex("dbo.DeelaspectRols", "Deelaspect_ID");
            AddForeignKey("dbo.DeelaspectRols", "Rol_ID", "dbo.Rols", "ID", cascadeDelete: true);
            AddForeignKey("dbo.DeelaspectRols", "Deelaspect_ID", "dbo.Deelaspects", "ID", cascadeDelete: true);
        }
    }
}
