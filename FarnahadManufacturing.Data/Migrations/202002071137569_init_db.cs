namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Configuration.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        AddressTypeId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Province = c.String(),
                        ZipCode = c.String(),
                        ResidentialAddress = c.Boolean(nullable: false),
                        IsDefaultAddress = c.Boolean(nullable: false),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                        Country_Id = c.Int(),
                        Company_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseConfiguration.AddressType", t => t.AddressTypeId, cascadeDelete: true)
                .ForeignKey("BaseConfiguration.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("BaseConfiguration.Country", t => t.Country_Id)
                .ForeignKey("Configuration.Company", t => t.Company_Id)
                .Index(t => t.AddressTypeId)
                .Index(t => t.CityId)
                .Index(t => t.Country_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "BaseConfiguration.AddressType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BaseConfiguration.City",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        CountryId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseConfiguration.Country", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "BaseConfiguration.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.ContactInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        ContactTypeId = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Address", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("BaseConfiguration.ContactType", t => t.ContactTypeId, cascadeDelete: true)
                .Index(t => t.ContactTypeId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "BaseConfiguration.ContactType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.Carrier",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Scac = c.String(maxLength: 4),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.CarrierService",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Code = c.String(nullable: false, maxLength: 16),
                        Carrier_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Carrier", t => t.Carrier_Id)
                .Index(t => t.Carrier_Id);
            
            CreateTable(
                "Configuration.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Logo = c.Binary(),
                        DefaultCarrierId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Carrier", t => t.DefaultCarrierId)
                .Index(t => t.DefaultCarrierId);
            
            CreateTable(
                "BaseConfiguration.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BaseConfiguration.FobType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.LocationGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseConfiguration.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "Configuration.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Number = c.String(),
                        Description = c.String(),
                        LocationTypeId = c.Int(nullable: false),
                        LocationGroupId = c.Int(nullable: false),
                        DefaultCustomerId = c.Int(),
                        AvailableForSale = c.Boolean(nullable: false),
                        Pickable = c.Boolean(nullable: false),
                        Receivable = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Customer", t => t.DefaultCustomerId)
                .ForeignKey("Configuration.LocationGroup", t => t.LocationGroupId, cascadeDelete: true)
                .ForeignKey("BaseConfiguration.LocationType", t => t.LocationTypeId, cascadeDelete: true)
                .Index(t => t.LocationTypeId)
                .Index(t => t.LocationGroupId)
                .Index(t => t.DefaultCustomerId);
            
            CreateTable(
                "BaseConfiguration.LocationType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        ReadOnly = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 256),
                        UserName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false, maxLength: 128),
                        PasswordSalt = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.Part",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BaseConfiguration.PaymentMethod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BaseConfiguration.PaymentTerm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.ProductCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        ParentProductCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.ProductCategory", t => t.ParentProductCategoryId)
                .Index(t => t.ParentProductCategoryId);
            
            CreateTable(
                "Configuration.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "BaseConfiguration.ShippingTerm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.Tracking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Abbreviation = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.Uom",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Abbreviation = c.String(maxLength: 4),
                        Description = c.String(),
                        Conversion = c.Double(nullable: false),
                        UomTypeId = c.Int(nullable: false),
                        ReadOnly = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseConfiguration.UomType", t => t.UomTypeId, cascadeDelete: true)
                .Index(t => t.UomTypeId);
            
            CreateTable(
                "BaseConfiguration.UomType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLocationGroups",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        LocationGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.LocationGroup_Id })
                .ForeignKey("Configuration.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("Configuration.LocationGroup", t => t.LocationGroup_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.LocationGroup_Id);
            
            CreateTable(
                "dbo.ProductProductCategories",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        ProductCategory_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.ProductCategory_Id })
                .ForeignKey("Configuration.Product", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("Configuration.ProductCategory", t => t.ProductCategory_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.ProductCategory_Id);
            
            CreateTable(
                "Configuration.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Company", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Configuration.Vendor",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Company", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Configuration.Vendor", "Id", "Configuration.Company");
            DropForeignKey("Configuration.Customer", "Id", "Configuration.Company");
            DropForeignKey("Configuration.Uom", "UomTypeId", "BaseConfiguration.UomType");
            DropForeignKey("dbo.ProductProductCategories", "ProductCategory_Id", "Configuration.ProductCategory");
            DropForeignKey("dbo.ProductProductCategories", "Product_Id", "Configuration.Product");
            DropForeignKey("Configuration.ProductCategory", "ParentProductCategoryId", "Configuration.ProductCategory");
            DropForeignKey("dbo.UserLocationGroups", "LocationGroup_Id", "Configuration.LocationGroup");
            DropForeignKey("dbo.UserLocationGroups", "User_Id", "Configuration.User");
            DropForeignKey("Configuration.Location", "LocationTypeId", "BaseConfiguration.LocationType");
            DropForeignKey("Configuration.Location", "LocationGroupId", "Configuration.LocationGroup");
            DropForeignKey("Configuration.Location", "DefaultCustomerId", "Configuration.Customer");
            DropForeignKey("Configuration.LocationGroup", "CategoryId", "BaseConfiguration.Category");
            DropForeignKey("Configuration.Company", "DefaultCarrierId", "Configuration.Carrier");
            DropForeignKey("Configuration.Address", "Company_Id", "Configuration.Company");
            DropForeignKey("Configuration.CarrierService", "Carrier_Id", "Configuration.Carrier");
            DropForeignKey("Configuration.ContactInformation", "ContactTypeId", "BaseConfiguration.ContactType");
            DropForeignKey("Configuration.ContactInformation", "AddressId", "Configuration.Address");
            DropForeignKey("BaseConfiguration.City", "CountryId", "BaseConfiguration.Country");
            DropForeignKey("Configuration.Address", "Country_Id", "BaseConfiguration.Country");
            DropForeignKey("Configuration.Address", "CityId", "BaseConfiguration.City");
            DropForeignKey("Configuration.Address", "AddressTypeId", "BaseConfiguration.AddressType");
            DropIndex("Configuration.Vendor", new[] { "Id" });
            DropIndex("Configuration.Customer", new[] { "Id" });
            DropIndex("dbo.ProductProductCategories", new[] { "ProductCategory_Id" });
            DropIndex("dbo.ProductProductCategories", new[] { "Product_Id" });
            DropIndex("dbo.UserLocationGroups", new[] { "LocationGroup_Id" });
            DropIndex("dbo.UserLocationGroups", new[] { "User_Id" });
            DropIndex("Configuration.Uom", new[] { "UomTypeId" });
            DropIndex("Configuration.ProductCategory", new[] { "ParentProductCategoryId" });
            DropIndex("Configuration.Location", new[] { "DefaultCustomerId" });
            DropIndex("Configuration.Location", new[] { "LocationGroupId" });
            DropIndex("Configuration.Location", new[] { "LocationTypeId" });
            DropIndex("Configuration.LocationGroup", new[] { "CategoryId" });
            DropIndex("Configuration.Company", new[] { "DefaultCarrierId" });
            DropIndex("Configuration.CarrierService", new[] { "Carrier_Id" });
            DropIndex("Configuration.ContactInformation", new[] { "AddressId" });
            DropIndex("Configuration.ContactInformation", new[] { "ContactTypeId" });
            DropIndex("BaseConfiguration.City", new[] { "CountryId" });
            DropIndex("Configuration.Address", new[] { "Company_Id" });
            DropIndex("Configuration.Address", new[] { "Country_Id" });
            DropIndex("Configuration.Address", new[] { "CityId" });
            DropIndex("Configuration.Address", new[] { "AddressTypeId" });
            DropTable("Configuration.Vendor");
            DropTable("Configuration.Customer");
            DropTable("dbo.ProductProductCategories");
            DropTable("dbo.UserLocationGroups");
            DropTable("BaseConfiguration.UomType");
            DropTable("Configuration.Uom");
            DropTable("Configuration.Tracking");
            DropTable("BaseConfiguration.ShippingTerm");
            DropTable("Configuration.Product");
            DropTable("Configuration.ProductCategory");
            DropTable("BaseConfiguration.PaymentTerm");
            DropTable("BaseConfiguration.PaymentMethod");
            DropTable("Configuration.Part");
            DropTable("Configuration.User");
            DropTable("BaseConfiguration.LocationType");
            DropTable("Configuration.Location");
            DropTable("Configuration.LocationGroup");
            DropTable("BaseConfiguration.FobType");
            DropTable("BaseConfiguration.Category");
            DropTable("Configuration.Company");
            DropTable("Configuration.CarrierService");
            DropTable("Configuration.Carrier");
            DropTable("BaseConfiguration.ContactType");
            DropTable("Configuration.ContactInformation");
            DropTable("BaseConfiguration.Country");
            DropTable("BaseConfiguration.City");
            DropTable("BaseConfiguration.AddressType");
            DropTable("Configuration.Address");
        }
    }
}
