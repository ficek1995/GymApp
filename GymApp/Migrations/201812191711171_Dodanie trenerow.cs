namespace GymApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Dodanietrenerow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsTrainer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsTrainer");
        }
    }
}
