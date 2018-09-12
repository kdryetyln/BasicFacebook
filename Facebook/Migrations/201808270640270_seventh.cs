namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seventh : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Connections", new[] { "UserName" });
            DropTable("dbo.Connections");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Connections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConnectionId = c.String(),
                        UserName = c.String(maxLength: 20),
                        IsOnline = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Connections", "UserName", unique: true);
        }
    }
}
