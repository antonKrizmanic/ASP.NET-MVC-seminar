namespace Seminar.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Year = c.Int(nullable: false),
                        Kilometer = c.Int(nullable: false),
                        FuelConsumption = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompanyID = c.Int(nullable: true),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        CreatedByUsername = c.String(),
                        UpdatedByUsername = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 100),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        CreatedByUsername = c.String(),
                        UpdatedByUsername = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        OIB = c.String(nullable: false, maxLength: 11),
                        CompanyID = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        CreatedByUsername = c.String(),
                        UpdatedByUsername = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.TravelWarrants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Relation = c.String(nullable: false),
                        Kilometer = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CarID = c.Int(nullable: false),
                        EmployeeID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(),
                        CreatedByUsername = c.String(),
                        UpdatedByUsername = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cars", t => t.CarID, cascadeDelete: false)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: false)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: false)
                .Index(t => t.CarID)
                .Index(t => t.EmployeeID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Cars", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.TravelWarrants", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.TravelWarrants", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.TravelWarrants", "CarID", "dbo.Cars");
            DropForeignKey("dbo.Employees", "CompanyID", "dbo.Companies");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TravelWarrants", new[] { "CompanyID" });
            DropIndex("dbo.TravelWarrants", new[] { "EmployeeID" });
            DropIndex("dbo.TravelWarrants", new[] { "CarID" });
            DropIndex("dbo.Employees", new[] { "CompanyID" });
            DropIndex("dbo.Cars", new[] { "CompanyID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TravelWarrants");
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
            DropTable("dbo.Cars");
        }
    }
}
