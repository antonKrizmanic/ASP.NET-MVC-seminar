namespace Seminar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tezina1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Employees", "Tezina");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Tezina", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
