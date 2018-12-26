namespace MobileSellingEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Locations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Province_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.Province_Id, cascadeDelete: true)
                .Index(t => t.Province_Id);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Int(),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "Province_Id", "dbo.Provinces");
            DropForeignKey("dbo.Provinces", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Provinces", new[] { "Country_Id" });
            DropIndex("dbo.Cities", new[] { "Province_Id" });
            DropTable("dbo.Countries");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
        }
    }
}
