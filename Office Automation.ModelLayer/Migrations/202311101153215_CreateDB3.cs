namespace Office_Automation.ModelLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Message", "UserSendMessage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_Message", "UserSendMessage", c => c.Int(nullable: false));
        }
    }
}
