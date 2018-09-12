namespace Facebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
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
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CommentUserID = c.Int(nullable: false),
                        PostID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                        Comment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: false)
                .ForeignKey("dbo.Comments", t => t.Comment_ID)
                .ForeignKey("dbo.Users", t => t.CommentUserID, cascadeDelete: false)
                .Index(t => t.CommentUserID)
                .Index(t => t.PostID)
                .Index(t => t.Comment_ID);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LikeUserID = c.Int(nullable: false),
                        PostID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                        Comment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.LikeUserID, cascadeDelete: false)
                .ForeignKey("dbo.Comments", t => t.Comment_ID)
                .Index(t => t.LikeUserID)
                .Index(t => t.PostID)
                .Index(t => t.Comment_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccountID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Accounts", t => t.AccountID, cascadeDelete: false)
                .Index(t => t.AccountID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        PostText = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: false)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ShUserID = c.Int(nullable: false),
                        ShPostID = c.Int(nullable: false),
                        NewPostID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Posts", t => t.ShPostID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.ShUserID, cascadeDelete: false)
                .Index(t => t.ShUserID)
                .Index(t => t.ShPostID);
            
            CreateTable(
                "dbo.RelationShips",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User1ID = c.Int(nullable: false),
                        User2ID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        ActionUserID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User1ID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User2ID, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.User1ID)
                .Index(t => t.User2ID)
                .Index(t => t.StatusID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        CreatedDate = c.DateTime(),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CommentUserID", "dbo.Users");
            DropForeignKey("dbo.Comments", "Comment_ID", "dbo.Comments");
            DropForeignKey("dbo.Likes", "Comment_ID", "dbo.Comments");
            DropForeignKey("dbo.Likes", "LikeUserID", "dbo.Users");
            DropForeignKey("dbo.RelationShips", "User_ID", "dbo.Users");
            DropForeignKey("dbo.RelationShips", "User2ID", "dbo.Users");
            DropForeignKey("dbo.RelationShips", "User1ID", "dbo.Users");
            DropForeignKey("dbo.RelationShips", "StatusID", "dbo.Status");
            DropForeignKey("dbo.Posts", "UserID", "dbo.Users");
            DropForeignKey("dbo.Shares", "ShUserID", "dbo.Users");
            DropForeignKey("dbo.Shares", "ShPostID", "dbo.Posts");
            DropForeignKey("dbo.Likes", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Users", "AccountID", "dbo.Accounts");
            DropIndex("dbo.RelationShips", new[] { "User_ID" });
            DropIndex("dbo.RelationShips", new[] { "StatusID" });
            DropIndex("dbo.RelationShips", new[] { "User2ID" });
            DropIndex("dbo.RelationShips", new[] { "User1ID" });
            DropIndex("dbo.Shares", new[] { "ShPostID" });
            DropIndex("dbo.Shares", new[] { "ShUserID" });
            DropIndex("dbo.Posts", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "AccountID" });
            DropIndex("dbo.Likes", new[] { "Comment_ID" });
            DropIndex("dbo.Likes", new[] { "PostID" });
            DropIndex("dbo.Likes", new[] { "LikeUserID" });
            DropIndex("dbo.Comments", new[] { "Comment_ID" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropIndex("dbo.Comments", new[] { "CommentUserID" });
            DropTable("dbo.Status");
            DropTable("dbo.RelationShips");
            DropTable("dbo.Shares");
            DropTable("dbo.Posts");
            DropTable("dbo.Users");
            DropTable("dbo.Likes");
            DropTable("dbo.Comments");
            DropTable("dbo.Accounts");
        }
    }
}
