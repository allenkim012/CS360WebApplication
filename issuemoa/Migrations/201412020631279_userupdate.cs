namespace issuemoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issue", "DonatedPoints", c => c.Int(nullable: false));
            AddColumn("dbo.User", "PointsGained", c => c.Int(nullable: false));
            DropColumn("dbo.Issue", "PointsGained");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issue", "PointsGained", c => c.Int(nullable: false));
            DropColumn("dbo.User", "PointsGained");
            DropColumn("dbo.Issue", "DonatedPoints");
        }
    }
}
