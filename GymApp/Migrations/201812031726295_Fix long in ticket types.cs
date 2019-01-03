namespace GymApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixlongintickettypes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes");
            DropIndex("dbo.Tickets", new[] { "TicketTypeId" });
            AddColumn("dbo.Tickets", "TicketType_Id", c => c.Long());
            AlterColumn("dbo.Tickets", "TicketTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "TicketType_Id");
            AddForeignKey("dbo.Tickets", "TicketType_Id", "dbo.TicketTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketType_Id", "dbo.TicketTypes");
            DropIndex("dbo.Tickets", new[] { "TicketType_Id" });
            AlterColumn("dbo.Tickets", "TicketTypeId", c => c.Long(nullable: false));
            DropColumn("dbo.Tickets", "TicketType_Id");
            CreateIndex("dbo.Tickets", "TicketTypeId");
            AddForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes", "Id", cascadeDelete: true);
        }
    }
}
