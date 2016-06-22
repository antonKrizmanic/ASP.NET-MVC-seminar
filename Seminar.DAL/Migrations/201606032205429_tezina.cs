namespace Seminar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tezina : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Tezina", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Tezina");
        }
    }
}
