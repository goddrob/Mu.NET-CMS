namespace Mu.NETcms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "GameId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "GameId");
        }
    }
}
