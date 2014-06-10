namespace BeoordelingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rols", "Deelaspect_ID", "dbo.Deelaspects");
            DropIndex("dbo.Rols", new[] { "Deelaspect_ID" });
            CreateTable(
                "dbo.DeelaspectRols",
                c => new
                    {
                        Deelaspect_ID = c.Int(nullable: false),
                        Rol_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Deelaspect_ID, t.Rol_ID })
                .ForeignKey("dbo.Deelaspects", t => t.Deelaspect_ID, cascadeDelete: true)
                .ForeignKey("dbo.Rols", t => t.Rol_ID, cascadeDelete: true)
                .Index(t => t.Deelaspect_ID)
                .Index(t => t.Rol_ID);
            
            DropColumn("dbo.Rols", "Deelaspect_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Rols", "Deelaspect_ID", c => c.Int());
            DropForeignKey("dbo.DeelaspectRols", "Rol_ID", "dbo.Rols");
            DropForeignKey("dbo.DeelaspectRols", "Deelaspect_ID", "dbo.Deelaspects");
            DropIndex("dbo.DeelaspectRols", new[] { "Rol_ID" });
            DropIndex("dbo.DeelaspectRols", new[] { "Deelaspect_ID" });
            DropTable("dbo.DeelaspectRols");
            CreateIndex("dbo.Rols", "Deelaspect_ID");
            AddForeignKey("dbo.Rols", "Deelaspect_ID", "dbo.Deelaspects", "ID");
        }
    }
}
