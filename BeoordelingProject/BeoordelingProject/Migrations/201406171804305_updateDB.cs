namespace BeoordelingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "academiejaar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "academiejaar");
        }
    }
}
