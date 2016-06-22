namespace Seminar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Grad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "City", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "City");
        }
    }
}
