namespace issuemoa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Role");
        }
    }
}
