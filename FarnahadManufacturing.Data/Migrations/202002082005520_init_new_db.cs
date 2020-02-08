namespace FarnahadManufacturing.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_new_db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Configuration.Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        AddressDetail = c.String(),
                        AddressTypeId = c.Int(nullable: false),
                        CountryId = c.Int(),
                        CityId = c.Int(),
                        Province = c.String(),
                        ZipCode = c.String(),
                        IsResidentialAddress = c.Boolean(nullable: false),
                        Latitude = c.Double(),
                        Longitude = c.Double(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseConfiguration.AddressType", t => t.AddressTypeId, cascadeDelete: true)
                .ForeignKey("BaseConfiguration.City", t => t.CityId)
                .ForeignKey("BaseConfiguration.Country", t => t.CountryId)
                .Index(t => t.AddressTypeId)
                .Index(t => t.CountryId)
                .Index(t => t.CityId);
            
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
                "Configuration.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Logo = c.Binary(),
                        DefaultCarrierId = c.Int(),
                        DefaultCarrierServiceId = c.Int(),
                        DefaultShippingTermId = c.Int(),
                        DefaultAddressId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Address", t => t.DefaultAddressId)
                .ForeignKey("Configuration.CarrierService", t => t.DefaultCarrierServiceId)
                .ForeignKey("Configuration.Carrier", t => t.DefaultCarrierId)
                .ForeignKey("BaseConfiguration.ShippingTerm", t => t.DefaultShippingTermId)
                .Index(t => t.DefaultCarrierId)
                .Index(t => t.DefaultCarrierServiceId)
                .Index(t => t.DefaultShippingTermId)
                .Index(t => t.DefaultAddressId);
            
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
                        CarrierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Carrier", t => t.CarrierId, cascadeDelete: true)
                .Index(t => t.CarrierId);
            
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
                "BaseConfiguration.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        ParentCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseConfiguration.Category", t => t.ParentCategoryId)
                .Index(t => t.ParentCategoryId);
            
            CreateTable(
                "Configuration.LocationGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        CategoryId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseConfiguration.Category", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "Configuration.Location",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Number = c.Int(nullable: false),
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
                .ForeignKey("BaseConfiguration.LocationType", t => t.LocationTypeId, cascadeDelete: true)
                .ForeignKey("Configuration.LocationGroup", t => t.LocationGroupId, cascadeDelete: true)
                .ForeignKey("Configuration.Customer", t => t.DefaultCustomerId)
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
                "Configuration.Part",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Number = c.String(nullable: false, maxLength: 64),
                        Description = c.String(),
                        Upc = c.String(nullable: false, maxLength: 32),
                        PartType = c.Int(nullable: false),
                        UomId = c.Int(nullable: false),
                        DefaultLocationId = c.Int(),
                        DefaultVendorId = c.Int(),
                        PickInPartUomOnly = c.Boolean(nullable: false),
                        Details = c.String(),
                        Picture = c.Binary(),
                        Length = c.Double(),
                        Width = c.Double(),
                        Height = c.Double(),
                        DistanceUomId = c.Int(nullable: false),
                        Weight = c.Double(),
                        WeightUomId = c.Int(nullable: false),
                        Cost = c.Double(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Vendor", t => t.DefaultVendorId)
                .ForeignKey("Configuration.Uom", t => t.DistanceUomId, cascadeDelete: true)
                .ForeignKey("Configuration.Uom", t => t.UomId, cascadeDelete: true)
                .ForeignKey("Configuration.Uom", t => t.WeightUomId, cascadeDelete: true)
                .ForeignKey("Configuration.Location", t => t.DefaultLocationId)
                .Index(t => t.UomId)
                .Index(t => t.DefaultLocationId)
                .Index(t => t.DefaultVendorId)
                .Index(t => t.DistanceUomId)
                .Index(t => t.WeightUomId);
            
            CreateTable(
                "BaseConfiguration.PaymentTerm",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        IsNet = c.Boolean(nullable: false),
                        NetNetDays = c.Int(),
                        NetDiscountPercentage = c.Double(),
                        NetDiscountDays = c.Double(),
                        IsDateDriven = c.Boolean(nullable: false),
                        DateDrivenDueDate = c.DateTime(),
                        DateDrivenNextMonthIfWithin = c.Int(),
                        DateDrivenDiscountPercentage = c.Double(),
                        DateDrivenDiscountDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
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
                "Configuration.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        PartId = c.Int(nullable: false),
                        Description = c.String(),
                        Detail = c.String(),
                        UomId = c.Int(nullable: false),
                        AllowToSellInOtherUom = c.Boolean(nullable: false),
                        CategoryId = c.Int(),
                        Picture = c.Binary(),
                        IsTaxable = c.Boolean(nullable: false),
                        ShowOnSaleOrder = c.Boolean(nullable: false),
                        Upc = c.String(maxLength: 32),
                        Sku = c.String(maxLength: 32),
                        Length = c.Double(),
                        Width = c.Double(),
                        Height = c.Double(),
                        DistanceUomId = c.Int(nullable: false),
                        Weight = c.Double(),
                        WeightUomId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Uom", t => t.DistanceUomId, cascadeDelete: true)
                .ForeignKey("Configuration.Uom", t => t.UomId, cascadeDelete: true)
                .ForeignKey("Configuration.Uom", t => t.WeightUomId, cascadeDelete: true)
                .ForeignKey("Configuration.Part", t => t.PartId, cascadeDelete: true)
                .ForeignKey("BaseConfiguration.Category", t => t.CategoryId)
                .Index(t => t.PartId)
                .Index(t => t.UomId)
                .Index(t => t.CategoryId)
                .Index(t => t.DistanceUomId)
                .Index(t => t.WeightUomId);
            
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
                "Configuration.ProductPrice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
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
                "Configuration.TrackingPart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrackingId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                        NextValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("BaseConfiguration.Tracking", t => t.TrackingId, cascadeDelete: true)
                .ForeignKey("Configuration.Part", t => t.PartId, cascadeDelete: true)
                .Index(t => t.TrackingId)
                .Index(t => t.PartId);
            
            CreateTable(
                "BaseConfiguration.Tracking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Abbreviation = c.String(nullable: false, maxLength: 4),
                        Description = c.String(),
                        TrackingValueType = c.Int(nullable: false),
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
                        Initial = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Password = c.String(nullable: false, maxLength: 128),
                        PasswordSalt = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.CustomerGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.TaxRate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Abbreviation = c.String(nullable: false, maxLength: 4),
                        IsPercentageSelected = c.Boolean(nullable: false),
                        Percentage = c.Double(),
                        FlatRate = c.Double(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Configuration.ContactInformation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false, maxLength: 64),
                        ContactType = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        AddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Address", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
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
                "BaseConfiguration.PaymentMethod",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompanyAddresses",
                c => new
                    {
                        Company_Id = c.Int(nullable: false),
                        Address_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Company_Id, t.Address_Id })
                .ForeignKey("Configuration.Company", t => t.Company_Id, cascadeDelete: true)
                .ForeignKey("Configuration.Address", t => t.Address_Id, cascadeDelete: true)
                .Index(t => t.Company_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "Configuration.ProductProductCategory",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCategoryId, t.ProductId })
                .ForeignKey("Configuration.ProductCategory", t => t.ProductCategoryId, cascadeDelete: true)
                .ForeignKey("Configuration.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Configuration.UserLocationGroup",
                c => new
                    {
                        LocationGroupId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LocationGroupId, t.UserId })
                .ForeignKey("Configuration.LocationGroup", t => t.LocationGroupId, cascadeDelete: true)
                .ForeignKey("Configuration.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.LocationGroupId)
                .Index(t => t.UserId);
            
            CreateTable(
                "Configuration.CustomerCustomerGroup",
                c => new
                    {
                        CustomerGroupId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomerGroupId, t.CustomerId })
                .ForeignKey("Configuration.CustomerGroup", t => t.CustomerGroupId, cascadeDelete: true)
                .ForeignKey("Configuration.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerGroupId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "Configuration.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CreditLimit = c.Double(),
                        DefaultPaymentTermId = c.Int(),
                        SalespersonId = c.Int(),
                        CategoryId = c.Int(),
                        AccountNumber = c.String(maxLength: 64),
                        TaxRateId = c.Int(nullable: false),
                        IsTaxExempt = c.Boolean(nullable: false),
                        TaxExemptNumber = c.String(maxLength: 64),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Company", t => t.Id)
                .ForeignKey("BaseConfiguration.PaymentTerm", t => t.DefaultPaymentTermId)
                .ForeignKey("Configuration.User", t => t.SalespersonId)
                .ForeignKey("BaseConfiguration.Category", t => t.CategoryId)
                .ForeignKey("Configuration.TaxRate", t => t.TaxRateId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.DefaultPaymentTermId)
                .Index(t => t.SalespersonId)
                .Index(t => t.CategoryId)
                .Index(t => t.TaxRateId);
            
            CreateTable(
                "Configuration.MyCompany",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        IsTaxExempt = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Company", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Configuration.Vendor",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AccountNumber = c.String(maxLength: 64),
                        MinOrderAmount = c.Double(),
                        DefaultPaymentTermId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Configuration.Company", t => t.Id)
                .ForeignKey("BaseConfiguration.PaymentTerm", t => t.DefaultPaymentTermId)
                .Index(t => t.Id)
                .Index(t => t.DefaultPaymentTermId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Configuration.Vendor", "DefaultPaymentTermId", "BaseConfiguration.PaymentTerm");
            DropForeignKey("Configuration.Vendor", "Id", "Configuration.Company");
            DropForeignKey("Configuration.MyCompany", "Id", "Configuration.Company");
            DropForeignKey("Configuration.Customer", "TaxRateId", "Configuration.TaxRate");
            DropForeignKey("Configuration.Customer", "CategoryId", "BaseConfiguration.Category");
            DropForeignKey("Configuration.Customer", "SalespersonId", "Configuration.User");
            DropForeignKey("Configuration.Customer", "DefaultPaymentTermId", "BaseConfiguration.PaymentTerm");
            DropForeignKey("Configuration.Customer", "Id", "Configuration.Company");
            DropForeignKey("Configuration.Address", "CountryId", "BaseConfiguration.Country");
            DropForeignKey("Configuration.ContactInformation", "AddressId", "Configuration.Address");
            DropForeignKey("Configuration.Location", "DefaultCustomerId", "Configuration.Customer");
            DropForeignKey("Configuration.CustomerCustomerGroup", "CustomerId", "Configuration.Customer");
            DropForeignKey("Configuration.CustomerCustomerGroup", "CustomerGroupId", "Configuration.CustomerGroup");
            DropForeignKey("Configuration.Product", "CategoryId", "BaseConfiguration.Category");
            DropForeignKey("BaseConfiguration.Category", "ParentCategoryId", "BaseConfiguration.Category");
            DropForeignKey("Configuration.LocationGroup", "CategoryId", "BaseConfiguration.Category");
            DropForeignKey("Configuration.UserLocationGroup", "UserId", "Configuration.User");
            DropForeignKey("Configuration.UserLocationGroup", "LocationGroupId", "Configuration.LocationGroup");
            DropForeignKey("Configuration.Location", "LocationGroupId", "Configuration.LocationGroup");
            DropForeignKey("Configuration.Part", "DefaultLocationId", "Configuration.Location");
            DropForeignKey("Configuration.Part", "WeightUomId", "Configuration.Uom");
            DropForeignKey("Configuration.Part", "UomId", "Configuration.Uom");
            DropForeignKey("Configuration.TrackingPart", "PartId", "Configuration.Part");
            DropForeignKey("Configuration.TrackingPart", "TrackingId", "BaseConfiguration.Tracking");
            DropForeignKey("Configuration.Product", "PartId", "Configuration.Part");
            DropForeignKey("Configuration.Part", "DistanceUomId", "Configuration.Uom");
            DropForeignKey("Configuration.Uom", "UomTypeId", "BaseConfiguration.UomType");
            DropForeignKey("Configuration.Product", "WeightUomId", "Configuration.Uom");
            DropForeignKey("Configuration.Product", "UomId", "Configuration.Uom");
            DropForeignKey("Configuration.ProductPrice", "ProductId", "Configuration.Product");
            DropForeignKey("Configuration.ProductProductCategory", "ProductId", "Configuration.Product");
            DropForeignKey("Configuration.ProductProductCategory", "ProductCategoryId", "Configuration.ProductCategory");
            DropForeignKey("Configuration.ProductCategory", "ParentProductCategoryId", "Configuration.ProductCategory");
            DropForeignKey("Configuration.Product", "DistanceUomId", "Configuration.Uom");
            DropForeignKey("Configuration.Part", "DefaultVendorId", "Configuration.Vendor");
            DropForeignKey("Configuration.Location", "LocationTypeId", "BaseConfiguration.LocationType");
            DropForeignKey("Configuration.Company", "DefaultShippingTermId", "BaseConfiguration.ShippingTerm");
            DropForeignKey("Configuration.Company", "DefaultCarrierId", "Configuration.Carrier");
            DropForeignKey("Configuration.CarrierService", "CarrierId", "Configuration.Carrier");
            DropForeignKey("Configuration.Company", "DefaultCarrierServiceId", "Configuration.CarrierService");
            DropForeignKey("Configuration.Company", "DefaultAddressId", "Configuration.Address");
            DropForeignKey("dbo.CompanyAddresses", "Address_Id", "Configuration.Address");
            DropForeignKey("dbo.CompanyAddresses", "Company_Id", "Configuration.Company");
            DropForeignKey("Configuration.Address", "CityId", "BaseConfiguration.City");
            DropForeignKey("BaseConfiguration.City", "CountryId", "BaseConfiguration.Country");
            DropForeignKey("Configuration.Address", "AddressTypeId", "BaseConfiguration.AddressType");
            DropIndex("Configuration.Vendor", new[] { "DefaultPaymentTermId" });
            DropIndex("Configuration.Vendor", new[] { "Id" });
            DropIndex("Configuration.MyCompany", new[] { "Id" });
            DropIndex("Configuration.Customer", new[] { "TaxRateId" });
            DropIndex("Configuration.Customer", new[] { "CategoryId" });
            DropIndex("Configuration.Customer", new[] { "SalespersonId" });
            DropIndex("Configuration.Customer", new[] { "DefaultPaymentTermId" });
            DropIndex("Configuration.Customer", new[] { "Id" });
            DropIndex("Configuration.CustomerCustomerGroup", new[] { "CustomerId" });
            DropIndex("Configuration.CustomerCustomerGroup", new[] { "CustomerGroupId" });
            DropIndex("Configuration.UserLocationGroup", new[] { "UserId" });
            DropIndex("Configuration.UserLocationGroup", new[] { "LocationGroupId" });
            DropIndex("Configuration.ProductProductCategory", new[] { "ProductId" });
            DropIndex("Configuration.ProductProductCategory", new[] { "ProductCategoryId" });
            DropIndex("dbo.CompanyAddresses", new[] { "Address_Id" });
            DropIndex("dbo.CompanyAddresses", new[] { "Company_Id" });
            DropIndex("Configuration.ContactInformation", new[] { "AddressId" });
            DropIndex("Configuration.TrackingPart", new[] { "PartId" });
            DropIndex("Configuration.TrackingPart", new[] { "TrackingId" });
            DropIndex("Configuration.ProductPrice", new[] { "ProductId" });
            DropIndex("Configuration.ProductCategory", new[] { "ParentProductCategoryId" });
            DropIndex("Configuration.Product", new[] { "WeightUomId" });
            DropIndex("Configuration.Product", new[] { "DistanceUomId" });
            DropIndex("Configuration.Product", new[] { "CategoryId" });
            DropIndex("Configuration.Product", new[] { "UomId" });
            DropIndex("Configuration.Product", new[] { "PartId" });
            DropIndex("Configuration.Uom", new[] { "UomTypeId" });
            DropIndex("Configuration.Part", new[] { "WeightUomId" });
            DropIndex("Configuration.Part", new[] { "DistanceUomId" });
            DropIndex("Configuration.Part", new[] { "DefaultVendorId" });
            DropIndex("Configuration.Part", new[] { "DefaultLocationId" });
            DropIndex("Configuration.Part", new[] { "UomId" });
            DropIndex("Configuration.Location", new[] { "DefaultCustomerId" });
            DropIndex("Configuration.Location", new[] { "LocationGroupId" });
            DropIndex("Configuration.Location", new[] { "LocationTypeId" });
            DropIndex("Configuration.LocationGroup", new[] { "CategoryId" });
            DropIndex("BaseConfiguration.Category", new[] { "ParentCategoryId" });
            DropIndex("Configuration.CarrierService", new[] { "CarrierId" });
            DropIndex("Configuration.Company", new[] { "DefaultAddressId" });
            DropIndex("Configuration.Company", new[] { "DefaultShippingTermId" });
            DropIndex("Configuration.Company", new[] { "DefaultCarrierServiceId" });
            DropIndex("Configuration.Company", new[] { "DefaultCarrierId" });
            DropIndex("BaseConfiguration.City", new[] { "CountryId" });
            DropIndex("Configuration.Address", new[] { "CityId" });
            DropIndex("Configuration.Address", new[] { "CountryId" });
            DropIndex("Configuration.Address", new[] { "AddressTypeId" });
            DropTable("Configuration.Vendor");
            DropTable("Configuration.MyCompany");
            DropTable("Configuration.Customer");
            DropTable("Configuration.CustomerCustomerGroup");
            DropTable("Configuration.UserLocationGroup");
            DropTable("Configuration.ProductProductCategory");
            DropTable("dbo.CompanyAddresses");
            DropTable("BaseConfiguration.PaymentMethod");
            DropTable("BaseConfiguration.FobType");
            DropTable("Configuration.ContactInformation");
            DropTable("Configuration.TaxRate");
            DropTable("Configuration.CustomerGroup");
            DropTable("Configuration.User");
            DropTable("BaseConfiguration.Tracking");
            DropTable("Configuration.TrackingPart");
            DropTable("BaseConfiguration.UomType");
            DropTable("Configuration.ProductPrice");
            DropTable("Configuration.ProductCategory");
            DropTable("Configuration.Product");
            DropTable("Configuration.Uom");
            DropTable("BaseConfiguration.PaymentTerm");
            DropTable("Configuration.Part");
            DropTable("BaseConfiguration.LocationType");
            DropTable("Configuration.Location");
            DropTable("Configuration.LocationGroup");
            DropTable("BaseConfiguration.Category");
            DropTable("BaseConfiguration.ShippingTerm");
            DropTable("Configuration.CarrierService");
            DropTable("Configuration.Carrier");
            DropTable("Configuration.Company");
            DropTable("BaseConfiguration.Country");
            DropTable("BaseConfiguration.City");
            DropTable("BaseConfiguration.AddressType");
            DropTable("Configuration.Address");
        }
    }
}
