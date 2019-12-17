namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCourseAssign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseAssigns",
                c => new
                    {
                        CourseAssignId = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        CreditTaken = c.Double(nullable: false),
                        CreditLeft = c.Double(nullable: false),
                        CourseID = c.Int(nullable: false),
                        Name = c.String(maxLength: 50, unicode: false),
                        Credit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CourseAssignId)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: false)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: false)
                .Index(t => t.DepartmentId)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseCode = c.String(nullable: false, maxLength: 100, unicode: false),
                        CourseName = c.String(nullable: false, maxLength: 8000, unicode: false),
                        CourseCredit = c.Double(nullable: false),
                        CourseDescription = c.String(nullable: false),
                        CourseAssignTo = c.String(),
                        CourseStatus = c.Boolean(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        SemesterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Semesters", t => t.SemesterId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.SemesterId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 7, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Semesters",
                c => new
                    {
                        SemesterId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.SemesterId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Address = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        ContactNo = c.String(nullable: false, maxLength: 14),
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
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        DesignationId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DesignationId);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        DayId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.DayId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseAssigns", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Teachers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseAssigns", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.CourseAssigns", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "SemesterId", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Students", new[] { "DepartmentId" });
            DropIndex("dbo.Teachers", new[] { "DepartmentId" });
            DropIndex("dbo.Teachers", new[] { "DesignationId" });
            DropIndex("dbo.Courses", new[] { "SemesterId" });
            DropIndex("dbo.Courses", new[] { "DepartmentId" });
            DropIndex("dbo.CourseAssigns", new[] { "CourseID" });
            DropIndex("dbo.CourseAssigns", new[] { "TeacherId" });
            DropIndex("dbo.CourseAssigns", new[] { "DepartmentId" });
            DropTable("dbo.Students");
            DropTable("dbo.Rooms");
            DropTable("dbo.Grades");
            DropTable("dbo.Days");
            DropTable("dbo.Designations");
            DropTable("dbo.Teachers");
            DropTable("dbo.Semesters");
            DropTable("dbo.Departments");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseAssigns");
        }
    }
}
