namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
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
                .ForeignKey("dbo.AddressTypes", t => t.AddressTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .Index(t => t.AddressTypeId)
                .Index(t => t.CityId)
                .Index(t => t.Country_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.AddressTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        CountryId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        ContactTypeId = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        AddressId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId_Id)
                .ForeignKey("dbo.ContactTypes", t => t.ContactTypeId, cascadeDelete: true)
                .Index(t => t.ContactTypeId)
                .Index(t => t.AddressId_Id);
            
            CreateTable(
                "dbo.ContactTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carriers",
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
                "dbo.CarrierServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Code = c.String(nullable: false, maxLength: 16),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Logo = c.Binary(),
                        DefaultCarrierId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carriers", t => t.DefaultCarrierId)
                .Index(t => t.DefaultCarrierId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FobTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocationGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Number = c.String(),
                        Description = c.String(),
                        LocationTypeId = c.Int(nullable: false),
                        LocationGroupId = c.Int(nullable: false),
                        AvailableForSale = c.Boolean(nullable: false),
                        Pickable = c.Boolean(nullable: false),
                        Receivable = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LocationGroups", t => t.LocationGroupId, cascadeDelete: true)
                .ForeignKey("dbo.LocationTypes", t => t.LocationTypeId, cascadeDelete: true)
                .Index(t => t.LocationTypeId)
                .Index(t => t.LocationGroupId);
            
            CreateTable(
                "dbo.LocationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        ReadOnly = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(),
                        UserName = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false, maxLength: 128),
                        PasswordSalt = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentTerms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        ParentProductCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ParentProductCategoryId)
                .Index(t => t.ParentProductCategoryId);
            
            CreateTable(
                "dbo.ProductTrees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_Id)
                .Index(t => t.ProductCategory_Id);
            
            CreateTable(
                "dbo.ShippingTerms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trackings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Abbreviation = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Uoms",
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
                .ForeignKey("dbo.UomTypes", t => t.UomTypeId, cascadeDelete: true)
                .Index(t => t.UomTypeId);
            
            CreateTable(
                "dbo.UomTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarrierServiceCarriers",
                c => new
                    {
                        CarrierService_Id = c.Int(nullable: false),
                        Carrier_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CarrierService_Id, t.Carrier_Id })
                .ForeignKey("dbo.CarrierServices", t => t.CarrierService_Id, cascadeDelete: true)
                .ForeignKey("dbo.Carriers", t => t.Carrier_Id, cascadeDelete: true)
                .Index(t => t.CarrierService_Id)
                .Index(t => t.Carrier_Id);
            
            CreateTable(
                "dbo.UserLocationGroups",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        LocationGroup_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.LocationGroup_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.LocationGroups", t => t.LocationGroup_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.LocationGroup_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uoms", "UomTypeId", "dbo.UomTypes");
            DropForeignKey("dbo.ProductTrees", "ProductCategory_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductCategories", "ParentProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.UserLocationGroups", "LocationGroup_Id", "dbo.LocationGroups");
            DropForeignKey("dbo.UserLocationGroups", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Locations", "LocationTypeId", "dbo.LocationTypes");
            DropForeignKey("dbo.Locations", "LocationGroupId", "dbo.LocationGroups");
            DropForeignKey("dbo.LocationGroups", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Companies", "DefaultCarrierId", "dbo.Carriers");
            DropForeignKey("dbo.Addresses", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.CarrierServiceCarriers", "Carrier_Id", "dbo.Carriers");
            DropForeignKey("dbo.CarrierServiceCarriers", "CarrierService_Id", "dbo.CarrierServices");
            DropForeignKey("dbo.ContactInformations", "ContactTypeId", "dbo.ContactTypes");
            DropForeignKey("dbo.ContactInformations", "AddressId_Id", "dbo.Addresses");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Addresses", "AddressTypeId", "dbo.AddressTypes");
            DropIndex("dbo.UserLocationGroups", new[] { "LocationGroup_Id" });
            DropIndex("dbo.UserLocationGroups", new[] { "User_Id" });
            DropIndex("dbo.CarrierServiceCarriers", new[] { "Carrier_Id" });
            DropIndex("dbo.CarrierServiceCarriers", new[] { "CarrierService_Id" });
            DropIndex("dbo.Uoms", new[] { "UomTypeId" });
            DropIndex("dbo.ProductTrees", new[] { "ProductCategory_Id" });
            DropIndex("dbo.ProductCategories", new[] { "ParentProductCategoryId" });
            DropIndex("dbo.Locations", new[] { "LocationGroupId" });
            DropIndex("dbo.Locations", new[] { "LocationTypeId" });
            DropIndex("dbo.LocationGroups", new[] { "CategoryId" });
            DropIndex("dbo.Companies", new[] { "DefaultCarrierId" });
            DropIndex("dbo.ContactInformations", new[] { "AddressId_Id" });
            DropIndex("dbo.ContactInformations", new[] { "ContactTypeId" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Addresses", new[] { "Company_Id" });
            DropIndex("dbo.Addresses", new[] { "Country_Id" });
            DropIndex("dbo.Addresses", new[] { "CityId" });
            DropIndex("dbo.Addresses", new[] { "AddressTypeId" });
            DropTable("dbo.UserLocationGroups");
            DropTable("dbo.CarrierServiceCarriers");
            DropTable("dbo.UomTypes");
            DropTable("dbo.Uoms");
            DropTable("dbo.Trackings");
            DropTable("dbo.ShippingTerms");
            DropTable("dbo.ProductTrees");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.PaymentTerms");
            DropTable("dbo.Users");
            DropTable("dbo.LocationTypes");
            DropTable("dbo.Locations");
            DropTable("dbo.LocationGroups");
            DropTable("dbo.FobTypes");
            DropTable("dbo.Categories");
            DropTable("dbo.Companies");
            DropTable("dbo.CarrierServices");
            DropTable("dbo.Carriers");
            DropTable("dbo.ContactTypes");
            DropTable("dbo.ContactInformations");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.AddressTypes");
            DropTable("dbo.Addresses");
        }
    }
}
