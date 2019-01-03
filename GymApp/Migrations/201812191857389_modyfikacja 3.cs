namespace GymApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modyfikacja3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "Title", c => c.String());
            DropColumn("dbo.Lessons", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lessons", "Name", c => c.String());
            DropColumn("dbo.Lessons", "Title");
        }
    }
}
