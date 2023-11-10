namespace Office_Automation.ModelLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_DB2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Message", "MessageTitle", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.T_Message", "MessageContent", c => c.String(nullable: false, maxLength: 700));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_Message", "MessageContent", c => c.String(nullable: false));
            AlterColumn("dbo.T_Message", "MessageTitle", c => c.String(nullable: false));
        }
    }
}
