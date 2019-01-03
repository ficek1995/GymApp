namespace GymApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Deleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Deleted");
        }
    }
}
