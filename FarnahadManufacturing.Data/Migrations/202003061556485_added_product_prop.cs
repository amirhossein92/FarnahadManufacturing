namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_product_prop : DbMigration
    {
        public override void Up()
        {
            AddColumn("Configuration.Product", "AlertNote", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Configuration.Product", "AlertNote");
        }
    }
}
