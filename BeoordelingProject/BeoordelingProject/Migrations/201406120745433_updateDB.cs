namespace BeoordelingProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resultaats", "CustomFeedback", c => c.String());
            AddColumn("dbo.Resultaats", "Breekpunten", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "MailZenden", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MailZenden");
            DropColumn("dbo.Resultaats", "Breekpunten");
            DropColumn("dbo.Resultaats", "CustomFeedback");
        }
    }
}
