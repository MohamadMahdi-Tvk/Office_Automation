namespace Office_Automation.ModelLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDb4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Letter", "LetterSave", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_Letter", "LetterSave");
        }
    }
}
