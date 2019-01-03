namespace GymApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TicketsModificationv2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tickets", name: "ApplicationUserId_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Tickets", name: "IX_ApplicationUserId_Id", newName: "IX_ApplicationUserId");
            AlterColumn("dbo.AspNetUsers", "TickedId", c => c.Long());
            DropColumn("dbo.Tickets", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.AspNetUsers", "TickedId", c => c.Int());
            RenameIndex(table: "dbo.Tickets", name: "IX_ApplicationUserId", newName: "IX_ApplicationUserId_Id");
            RenameColumn(table: "dbo.Tickets", name: "ApplicationUserId", newName: "ApplicationUserId_Id");
        }
    }
}
