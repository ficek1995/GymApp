namespace GymApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketsandTicketsTypes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TicketTypeId = c.Long(nullable: false),
                        UserId = c.Long(nullable: false),
                        AssignTime = c.DateTime(nullable: false),
                        AssignTo = c.DateTime(nullable: false),
                        ApplicationUserId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId_Id)
                .ForeignKey("dbo.TicketTypes", t => t.TicketTypeId, cascadeDelete: true)
                .Index(t => t.TicketTypeId)
                .Index(t => t.ApplicationUserId_Id);
            
            CreateTable(
                "dbo.TicketTypes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.String(),
                        TicketCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "TickedId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "TicketTypeId", "dbo.TicketTypes");
            DropForeignKey("dbo.Tickets", "ApplicationUserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tickets", new[] { "ApplicationUserId_Id" });
            DropIndex("dbo.Tickets", new[] { "TicketTypeId" });
            DropColumn("dbo.AspNetUsers", "TickedId");
            DropTable("dbo.TicketTypes");
            DropTable("dbo.Tickets");
        }
    }
}
