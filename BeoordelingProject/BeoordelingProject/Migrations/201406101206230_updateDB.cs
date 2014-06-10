namespace BeoordelingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resultaats", "CFaanwezig", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resultaats", "CFaanwezig");
        }
    }
}
