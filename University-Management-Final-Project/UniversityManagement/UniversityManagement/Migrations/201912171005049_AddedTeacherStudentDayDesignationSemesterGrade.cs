namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTeacherStudentDayDesignationSemesterGrade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DayId);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.GradeId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        RegNo = c.String(maxLength: 50, unicode: false),
                        Email = c.String(nullable: false),
                        ContactNo = c.String(nullable: false, maxLength: 14),
                        Date = c.DateTime(nullable: false),
                        Address = c.String(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        TeacherName = c.String(nullable: false, maxLength: 50, unicode: false),
                        TeacherAddress = c.String(nullable: false),
                        TeacherEmail = c.String(nullable: false, maxLength: 50, unicode: false),
                        TeacherContactNo = c.String(nullable: false, maxLength: 14),
                        DesignationId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        CreditTaken = c.Double(nullable: false),
                        CreditLeft = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.DesignationId)
                .Index(t => t.DepartmentId);
            
            AddColumn("dbo.Semesters", "Name", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.Semesters", "SemesterName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Semesters", "SemesterName", c => c.String());
            DropForeignKey("dbo.Teachers", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Teachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Teachers", new[] { "DepartmentId" });
            DropIndex("dbo.Teachers", new[] { "DesignationId" });
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            DropColumn("dbo.Semesters", "Name");
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Rooms");
            DropTable("dbo.Grades");
            DropTable("dbo.Designations");
            DropTable("dbo.Days");
        }
    }
}
