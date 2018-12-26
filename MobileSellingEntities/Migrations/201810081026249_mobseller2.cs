namespace MobileSellingEntities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mobseller2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "ImageUrl", c => c.String(maxLength: 300, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Departments", "ImageUrl");
        }
    }
}
