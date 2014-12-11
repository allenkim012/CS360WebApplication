namespace issuemoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Board",
                c => new
                    {
                        BoardId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 10),
                        IssueId = c.Int(nullable: false),
                        BoardTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BoardId)
                .ForeignKey("dbo.BoardType", t => t.BoardTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Issue", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.IssueId)
                .Index(t => t.BoardTypeId);
            
            CreateTable(
                "dbo.BoardType",
                c => new
                    {
                        BoardTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BoardTypeId);
            
            CreateTable(
                "dbo.Issue",
                c => new
                    {
                        IssueId = c.Int(nullable: false, identity: true),
                        IssueTitle = c.String(nullable: false, maxLength: 30),
                        ImageURL = c.String(maxLength: 30),
                        StartDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        DonatedPoints = c.Int(nullable: false),
                        IsTopIssue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IssueId);
            
            CreateTable(
                "dbo.PointHistory",
                c => new
                    {
                        PointHistoryId = c.Int(nullable: false, identity: true),
                        ChangeAmount = c.Int(nullable: false),
                        ChangeDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        PointTypeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IssueId = c.Int(),
                    })
                .PrimaryKey(t => t.PointHistoryId)
                .ForeignKey("dbo.Issue", t => t.IssueId)
                .ForeignKey("dbo.PointType", t => t.PointTypeId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.PointTypeId)
                .Index(t => t.UserId)
                .Index(t => t.IssueId);
            
            CreateTable(
                "dbo.PointType",
                c => new
                    {
                        PointTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PointTypeId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 40),
                        Password = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 10),
                        Email = c.String(nullable: false, maxLength: 50),
                        PointsGained = c.Int(nullable: false),
                        PointsDonated = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Forum",
                c => new
                    {
                        ForumId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Text = c.String(nullable: false),
                        UploadDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        ImageUrl = c.String(),
                        LikeCount = c.Int(nullable: false),
                        HateCount = c.Int(nullable: false),
                        ViewCount = c.Int(nullable: false),
                        WriterId = c.Int(nullable: false),
                        BoardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ForumId)
                .ForeignKey("dbo.Board", t => t.BoardId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.WriterId, cascadeDelete: true)
                .Index(t => t.WriterId)
                .Index(t => t.BoardId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        UploadDate = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Text = c.String(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        HateCount = c.Int(nullable: false),
                        WriterId = c.Int(nullable: false),
                        ForumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Forum", t => t.ForumId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.WriterId, cascadeDelete: false)
                .Index(t => t.WriterId)
                .Index(t => t.ForumId);
            
            CreateTable(
                "dbo.IssueSummary",
                c => new
                    {
                        IssueSummaryId = c.Int(nullable: false, identity: true),
                        SummaryTitle = c.String(nullable: false),
                        Summary = c.String(),
                        IssueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IssueSummaryId)
                .ForeignKey("dbo.Issue", t => t.IssueId, cascadeDelete: true)
                .Index(t => t.IssueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssueSummary", "IssueId", "dbo.Issue");
            DropForeignKey("dbo.Comment", "WriterId", "dbo.User");
            DropForeignKey("dbo.Comment", "ForumId", "dbo.Forum");
            DropForeignKey("dbo.Forum", "WriterId", "dbo.User");
            DropForeignKey("dbo.Forum", "BoardId", "dbo.Board");
            DropForeignKey("dbo.Board", "IssueId", "dbo.Issue");
            DropForeignKey("dbo.PointHistory", "UserId", "dbo.User");
            DropForeignKey("dbo.PointHistory", "PointTypeId", "dbo.PointType");
            DropForeignKey("dbo.PointHistory", "IssueId", "dbo.Issue");
            DropForeignKey("dbo.Board", "BoardTypeId", "dbo.BoardType");
            DropIndex("dbo.IssueSummary", new[] { "IssueId" });
            DropIndex("dbo.Comment", new[] { "ForumId" });
            DropIndex("dbo.Comment", new[] { "WriterId" });
            DropIndex("dbo.Forum", new[] { "BoardId" });
            DropIndex("dbo.Forum", new[] { "WriterId" });
            DropIndex("dbo.PointHistory", new[] { "IssueId" });
            DropIndex("dbo.PointHistory", new[] { "UserId" });
            DropIndex("dbo.PointHistory", new[] { "PointTypeId" });
            DropIndex("dbo.Board", new[] { "BoardTypeId" });
            DropIndex("dbo.Board", new[] { "IssueId" });
            DropTable("dbo.IssueSummary");
            DropTable("dbo.Comment");
            DropTable("dbo.Forum");
            DropTable("dbo.User");
            DropTable("dbo.PointType");
            DropTable("dbo.PointHistory");
            DropTable("dbo.Issue");
            DropTable("dbo.BoardType");
            DropTable("dbo.Board");
        }
    }
}
