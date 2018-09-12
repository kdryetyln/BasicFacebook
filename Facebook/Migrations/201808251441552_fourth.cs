namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(),
                        UserID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        NotifyText = c.String(),
                        IsShow = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notifications");
        }
    }
}
