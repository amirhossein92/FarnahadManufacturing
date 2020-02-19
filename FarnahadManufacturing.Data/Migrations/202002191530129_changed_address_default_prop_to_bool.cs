namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_address_default_prop_to_bool : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Configuration.Company", "DefaultAddress_Id", "Configuration.Address");
            DropIndex("Configuration.Company", new[] { "DefaultAddress_Id" });
            AddColumn("Configuration.Address", "IsDefaultAddress", c => c.Boolean(nullable: false));
            DropColumn("Configuration.Address", "CompanyDefaultAddressId");
            DropColumn("Configuration.Company", "DefaultAddressId");
            DropColumn("Configuration.Company", "DefaultAddress_Id");
        }
        
        public override void Down()
        {
            AddColumn("Configuration.Company", "DefaultAddress_Id", c => c.Int());
            AddColumn("Configuration.Company", "DefaultAddressId", c => c.Int());
            AddColumn("Configuration.Address", "CompanyDefaultAddressId", c => c.Int(nullable: false));
            DropColumn("Configuration.Address", "IsDefaultAddress");
            CreateIndex("Configuration.Company", "DefaultAddress_Id");
            AddForeignKey("Configuration.Company", "DefaultAddress_Id", "Configuration.Address", "Id");
        }
    }
}
