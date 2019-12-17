namespace UniversityManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyTeachrModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "Name", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Teachers", "Address", c => c.String(nullable: false));
            AddColumn("dbo.Teachers", "Email", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Teachers", "ContactNo", c => c.String(nullable: false, maxLength: 14));
            DropColumn("dbo.Teachers", "TeacherName");
            DropColumn("dbo.Teachers", "TeacherAddress");
            DropColumn("dbo.Teachers", "TeacherEmail");
            DropColumn("dbo.Teachers", "TeacherContactNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "TeacherContactNo", c => c.String(nullable: false, maxLength: 14));
            AddColumn("dbo.Teachers", "TeacherEmail", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Teachers", "TeacherAddress", c => c.String(nullable: false));
            AddColumn("dbo.Teachers", "TeacherName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.Teachers", "ContactNo");
            DropColumn("dbo.Teachers", "Email");
            DropColumn("dbo.Teachers", "Address");
            DropColumn("dbo.Teachers", "Name");
        }
    }
}
