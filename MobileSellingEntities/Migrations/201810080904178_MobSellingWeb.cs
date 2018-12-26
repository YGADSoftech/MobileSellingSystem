namespace MobileSellingEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MobSellingWeb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        ImageUrl = c.String(maxLength: 300, unicode: false),
                        Department_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id, cascadeDelete: true)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        Price = c.Single(nullable: false),
                        Description = c.String(maxLength: 100, unicode: false),
                        LaunchDate = c.DateTime(),
                        Condition_Id = c.Int(),
                        SubCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conditions", t => t.Condition_Id)
                .ForeignKey("dbo.SubCategories", t => t.SubCategory_Id, cascadeDelete: true)
                .Index(t => t.Condition_Id)
                .Index(t => t.SubCategory_Id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Caption = c.String(maxLength: 100, unicode: false),
                        Priority = c.Int(nullable: false),
                        Url = c.String(nullable: false, maxLength: 300, unicode: false),
                        Products_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Products_Id)
                .Index(t => t.Products_Id);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                        ImageUrl = c.String(maxLength: 300, unicode: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.ProductsColors",
                c => new
                    {
                        Products_Id = c.Int(nullable: false),
                        Colors_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Products_Id, t.Colors_Id })
                .ForeignKey("dbo.Products", t => t.Products_Id, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.Colors_Id, cascadeDelete: true)
                .Index(t => t.Products_Id)
                .Index(t => t.Colors_Id);
            
            CreateTable(
                "dbo.ProductsSizes",
                c => new
                    {
                        Products_Id = c.Int(nullable: false),
                        Sizes_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Products_Id, t.Sizes_Id })
                .ForeignKey("dbo.Products", t => t.Products_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.Sizes_Id, cascadeDelete: true)
                .Index(t => t.Products_Id)
                .Index(t => t.Sizes_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "SubCategory_Id", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.ProductsSizes", "Sizes_Id", "dbo.Sizes");
            DropForeignKey("dbo.ProductsSizes", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Condition_Id", "dbo.Conditions");
            DropForeignKey("dbo.ProductsColors", "Colors_Id", "dbo.Colors");
            DropForeignKey("dbo.ProductsColors", "Products_Id", "dbo.Products");
            DropForeignKey("dbo.Categories", "Department_Id", "dbo.Departments");
            DropIndex("dbo.ProductsSizes", new[] { "Sizes_Id" });
            DropIndex("dbo.ProductsSizes", new[] { "Products_Id" });
            DropIndex("dbo.ProductsColors", new[] { "Colors_Id" });
            DropIndex("dbo.ProductsColors", new[] { "Products_Id" });
            DropIndex("dbo.SubCategories", new[] { "Category_Id" });
            DropIndex("dbo.ProductImages", new[] { "Products_Id" });
            DropIndex("dbo.Products", new[] { "SubCategory_Id" });
            DropIndex("dbo.Products", new[] { "Condition_Id" });
            DropIndex("dbo.Categories", new[] { "Department_Id" });
            DropTable("dbo.ProductsSizes");
            DropTable("dbo.ProductsColors");
            DropTable("dbo.SubCategories");
            DropTable("dbo.Sizes");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Products");
            DropTable("dbo.Conditions");
            DropTable("dbo.Colors");
            DropTable("dbo.Departments");
            DropTable("dbo.Categories");
        }
    }
}
