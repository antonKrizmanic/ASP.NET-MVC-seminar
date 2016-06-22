namespace Seminar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OibOpis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "OIB", c => c.String(nullable: false, maxLength: 11));
            AddColumn("dbo.TravelWarrants", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TravelWarrants", "Description");
            DropColumn("dbo.Companies", "OIB");
        }
    }
}
