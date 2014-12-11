namespace issuemoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedDonatedPoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issue", "DonatedPoints", c => c.Int(nullable: false));
            DropColumn("dbo.Issue", "ImageURL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issue", "ImageURL", c => c.String());
            DropColumn("dbo.Issue", "DonatedPoints");
        }
    }
}
