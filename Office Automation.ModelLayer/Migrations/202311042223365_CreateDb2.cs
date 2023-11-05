namespace Office_Automation.ModelLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Department", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.T_Department", "Info", c => c.String(maxLength: 700));
            AlterColumn("dbo.T_Letter", "Title", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.T_Letter", "Number", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.T_Letter", "Type", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.T_Role", "Name", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.T_Role", "Title", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.T_User", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.T_User", "Family", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_User", "Family", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.T_User", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.T_Role", "Title", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.T_Role", "Name", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.T_Letter", "Type", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.T_Letter", "Number", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.T_Letter", "Title", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.T_Department", "Info", c => c.String(maxLength: 300));
            AlterColumn("dbo.T_Department", "Name", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
