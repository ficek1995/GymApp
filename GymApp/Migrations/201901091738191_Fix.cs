namespace GymApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LessonsUsers", new[] { "UserRefId" });
            DropIndex("dbo.LessonsUsers", new[] { "LessonRefId" });
            RenameColumn(table: "dbo.LessonsUsers", name: "UserRefId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.LessonsUsers", name: "LessonRefId", newName: "UserRefId");
            RenameColumn(table: "dbo.LessonsUsers", name: "__mig_tmp__0", newName: "LessonRefId");
            DropPrimaryKey("dbo.LessonsUsers");
            AlterColumn("dbo.LessonsUsers", "UserRefId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.LessonsUsers", "LessonRefId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.LessonsUsers", new[] { "LessonRefId", "UserRefId" });
            CreateIndex("dbo.LessonsUsers", "LessonRefId");
            CreateIndex("dbo.LessonsUsers", "UserRefId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LessonsUsers", new[] { "UserRefId" });
            DropIndex("dbo.LessonsUsers", new[] { "LessonRefId" });
            DropPrimaryKey("dbo.LessonsUsers");
            AlterColumn("dbo.LessonsUsers", "LessonRefId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.LessonsUsers", "UserRefId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.LessonsUsers", new[] { "UserRefId", "LessonRefId" });
            RenameColumn(table: "dbo.LessonsUsers", name: "LessonRefId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.LessonsUsers", name: "UserRefId", newName: "LessonRefId");
            RenameColumn(table: "dbo.LessonsUsers", name: "__mig_tmp__0", newName: "UserRefId");
            CreateIndex("dbo.LessonsUsers", "LessonRefId");
            CreateIndex("dbo.LessonsUsers", "UserRefId");
        }
    }
}
