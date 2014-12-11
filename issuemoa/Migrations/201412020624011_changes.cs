namespace issuemoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issue", "PointsGained", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issue", "PointsGained");
        }
    }
}
