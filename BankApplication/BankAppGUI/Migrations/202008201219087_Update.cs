namespace BankAppGUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountDatas", "Date", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountDatas", "Date");
        }
    }
}
