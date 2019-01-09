namespace GymApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LessonsUsers",
                c => new
                    {
                        UserRefId = c.Long(nullable: false),
                        LessonRefId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.LessonRefId })
                .ForeignKey("dbo.Lessons", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.LessonRefId, cascadeDelete: true)
                .Index(t => t.UserRefId)
                .Index(t => t.LessonRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LessonsUsers", "LessonRefId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LessonsUsers", "UserRefId", "dbo.Lessons");
            DropIndex("dbo.LessonsUsers", new[] { "LessonRefId" });
            DropIndex("dbo.LessonsUsers", new[] { "UserRefId" });
            DropTable("dbo.LessonsUsers");
        }
    }
}
