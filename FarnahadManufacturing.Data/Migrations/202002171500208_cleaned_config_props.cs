namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cleaned_config_props : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("Configuration.CustomerCustomerGroup");
            DropPrimaryKey("Configuration.ProductProductCategory");
            AddPrimaryKey("Configuration.CustomerCustomerGroup", new[] { "CustomerId", "CustomerGroupId" });
            AddPrimaryKey("Configuration.ProductProductCategory", new[] { "ProductId", "ProductCategoryId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("Configuration.ProductProductCategory");
            DropPrimaryKey("Configuration.CustomerCustomerGroup");
            AddPrimaryKey("Configuration.ProductProductCategory", new[] { "ProductCategoryId", "ProductId" });
            AddPrimaryKey("Configuration.CustomerCustomerGroup", new[] { "CustomerGroupId", "CustomerId" });
        }
    }
}
