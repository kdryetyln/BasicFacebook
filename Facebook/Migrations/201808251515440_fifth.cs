namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fifth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "PostUserID", c => c.Int(nullable: false));
            AddColumn("dbo.Notifications", "ActionID", c => c.Int(nullable: false));
            DropColumn("dbo.Notifications", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notifications", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Notifications", "ActionID");
            DropColumn("dbo.Notifications", "PostUserID");
        }
    }
}
