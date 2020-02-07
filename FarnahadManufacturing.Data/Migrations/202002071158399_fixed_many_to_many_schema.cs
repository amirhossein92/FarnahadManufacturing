namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixed_many_to_many_schema : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserLocationGroups", newName: "UserLocationGroup");
            RenameTable(name: "dbo.ProductProductCategories", newName: "ProductProductCategory");
            MoveTable(name: "dbo.UserLocationGroup", newSchema: "Configuration");
            MoveTable(name: "dbo.ProductProductCategory", newSchema: "Configuration");
            RenameColumn(table: "Configuration.UserLocationGroup", name: "User_Id", newName: "UserId");
            RenameColumn(table: "Configuration.UserLocationGroup", name: "LocationGroup_Id", newName: "LocationGroupId");
            RenameColumn(table: "Configuration.ProductProductCategory", name: "Product_Id", newName: "ProductId");
            RenameColumn(table: "Configuration.ProductProductCategory", name: "ProductCategory_Id", newName: "ProductCategoryId");
            RenameIndex(table: "Configuration.UserLocationGroup", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "Configuration.UserLocationGroup", name: "IX_LocationGroup_Id", newName: "IX_LocationGroupId");
            RenameIndex(table: "Configuration.ProductProductCategory", name: "IX_Product_Id", newName: "IX_ProductId");
            RenameIndex(table: "Configuration.ProductProductCategory", name: "IX_ProductCategory_Id", newName: "IX_ProductCategoryId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "Configuration.ProductProductCategory", name: "IX_ProductCategoryId", newName: "IX_ProductCategory_Id");
            RenameIndex(table: "Configuration.ProductProductCategory", name: "IX_ProductId", newName: "IX_Product_Id");
            RenameIndex(table: "Configuration.UserLocationGroup", name: "IX_LocationGroupId", newName: "IX_LocationGroup_Id");
            RenameIndex(table: "Configuration.UserLocationGroup", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "Configuration.ProductProductCategory", name: "ProductCategoryId", newName: "ProductCategory_Id");
            RenameColumn(table: "Configuration.ProductProductCategory", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "Configuration.UserLocationGroup", name: "LocationGroupId", newName: "LocationGroup_Id");
            RenameColumn(table: "Configuration.UserLocationGroup", name: "UserId", newName: "User_Id");
            MoveTable(name: "Configuration.ProductProductCategory", newSchema: "dbo");
            MoveTable(name: "Configuration.UserLocationGroup", newSchema: "dbo");
            RenameTable(name: "dbo.ProductProductCategory", newName: "ProductProductCategories");
            RenameTable(name: "dbo.UserLocationGroup", newName: "UserLocationGroups");
        }
    }
}
