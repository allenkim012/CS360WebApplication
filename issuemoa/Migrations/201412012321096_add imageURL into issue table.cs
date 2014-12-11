namespace issuemoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimageURLintoissuetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Issue", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Issue", "ImageURL");
        }
    }
}
