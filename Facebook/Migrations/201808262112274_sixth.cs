namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixth : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.ID)
                .Index(t => t.UserName, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Connections", new[] { "UserName" });
            DropTable("dbo.Connections");
        }
    }
}
