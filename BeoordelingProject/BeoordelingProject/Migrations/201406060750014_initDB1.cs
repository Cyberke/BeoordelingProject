namespace BeoordelingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HoofdaspectResultaats", "RolId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HoofdaspectResultaats", "RolId");
        }
    }
}
