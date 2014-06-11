namespace BeoordelingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HoofdaspectResultaats", "Rol_ID", c => c.Int());
            CreateIndex("dbo.HoofdaspectResultaats", "Rol_ID");
            AddForeignKey("dbo.HoofdaspectResultaats", "Rol_ID", "dbo.Rols", "ID");
            DropColumn("dbo.HoofdaspectResultaats", "RolId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HoofdaspectResultaats", "RolId", c => c.Int(nullable: false));
            DropForeignKey("dbo.HoofdaspectResultaats", "Rol_ID", "dbo.Rols");
            DropIndex("dbo.HoofdaspectResultaats", new[] { "Rol_ID" });
            DropColumn("dbo.HoofdaspectResultaats", "Rol_ID");
        }
    }
}
