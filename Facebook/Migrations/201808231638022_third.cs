namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CommentText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "CommentText");
        }
    }
}
