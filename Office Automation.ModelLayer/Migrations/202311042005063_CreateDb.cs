namespace Office_Automation.ModelLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_Department",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Info = c.String(maxLength: 300),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.T_Letter",
                c => new
                    {
                        LetterId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        LetterContent = c.String(nullable: false, maxLength: 1000),
                        Number = c.String(nullable: false, maxLength: 20),
                        SendDate = c.DateTime(nullable: false),
                        Type = c.String(nullable: false, maxLength: 10),
                        DepartmentId = c.Int(nullable: false),
                        SendDepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LetterId)
                .ForeignKey("dbo.T_Department", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.T_Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        Title = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.T_User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Family = c.String(nullable: false, maxLength: 30),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Boolean(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 15, unicode: false),
                        PersonnelID = c.String(nullable: false, maxLength: 10),
                        StartingDate = c.DateTime(nullable: false),
                        Password = c.String(nullable: false, maxLength: 20),
                        ProfileImage = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.T_Department", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.T_Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.DepartmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_User", "RoleId", "dbo.T_Role");
            DropForeignKey("dbo.T_User", "DepartmentId", "dbo.T_Department");
            DropForeignKey("dbo.T_Letter", "DepartmentId", "dbo.T_Department");
            DropIndex("dbo.T_User", new[] { "DepartmentId" });
            DropIndex("dbo.T_User", new[] { "RoleId" });
            DropIndex("dbo.T_Letter", new[] { "DepartmentId" });
            DropTable("dbo.T_User");
            DropTable("dbo.T_Role");
            DropTable("dbo.T_Letter");
            DropTable("dbo.T_Department");
        }
    }
}
