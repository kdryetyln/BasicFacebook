namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "AccountID", "dbo.Accounts");
            DropIndex("dbo.Users", new[] { "AccountID" });
            AddColumn("dbo.Users", "Name", c => c.String());
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "Password", c => c.String());
            DropColumn("dbo.Users", "AccountID");
            DropTable("dbo.Accounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Users", "AccountID", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "Name");
            CreateIndex("dbo.Users", "AccountID");
            AddForeignKey("dbo.Users", "AccountID", "dbo.Accounts", "ID", cascadeDelete: true);
        }
    }
}
