namespace Seminar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kilometraza : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TravelWarrants", "StartKilometer", c => c.Int(nullable: false));
            AddColumn("dbo.TravelWarrants", "EndKilometer", c => c.Int(nullable: false));
            DropColumn("dbo.Cars", "Kilometer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "Kilometer", c => c.Int(nullable: false));
            DropColumn("dbo.TravelWarrants", "EndKilometer");
            DropColumn("dbo.TravelWarrants", "StartKilometer");
        }
    }
}
