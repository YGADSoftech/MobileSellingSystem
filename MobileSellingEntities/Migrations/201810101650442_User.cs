namespace MobileSellingEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20, unicode: false),
                        Password = c.String(nullable: false, maxLength: 20, unicode: false),
                        ContactNumber = c.String(maxLength: 20, unicode: false),
                        Email = c.String(maxLength: 255, unicode: false),
                        ImageUrl = c.String(maxLength: 255, unicode: false),
                        BirthDate = c.DateTime(),
                        LastName = c.String(maxLength: 100, unicode: false),
                        SecurityQuestion = c.String(maxLength: 255, unicode: false),
                        SecurityAnswer = c.String(maxLength: 255, unicode: false),
                        Address_Id = c.Int(),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Address_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetAddress = c.String(nullable: false, maxLength: 255, unicode: false),
                        City_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id, cascadeDelete: true)
                .Index(t => t.City_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "Address_Id" });
            DropTable("dbo.Addresses");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
        }
    }
}
