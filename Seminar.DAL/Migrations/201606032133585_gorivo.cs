namespace Seminar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gorivo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cars", "FuelConsumption", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cars", "FuelConsumption", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
