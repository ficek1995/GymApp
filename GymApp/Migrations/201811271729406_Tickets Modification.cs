namespace GymApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketsModification : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TicketTypes", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TicketTypes", "Price", c => c.String());
        }
    }
}
