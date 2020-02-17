using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Data.Configs.BaseConfiguration;
using FarnahadManufacturing.Data.Configs.Configuration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;
using AddressTypeConfiguration = FarnahadManufacturing.Data.Configs.BaseConfiguration.AddressTypeConfiguration;

namespace FarnahadManufacturing.Data
{
    public class FarnahadManufacturingDbContext : DbContext
    {
        public FarnahadManufacturingDbContext() : base("FarnahadManufacturingConnectionString")
        {
        }

        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<FobType> FobTypes { get; set; }
        public virtual DbSet<LocationType> LocationTypes { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PaymentTerm> PaymentTerms { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<ShippingTerm> ShippingTerms { get; set; }
        public virtual DbSet<Tracking> Trackings { get; set; }
        public virtual DbSet<UomType> UomTypes { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<CarrierService> CarrierServices { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ContactInformation> ContactInformations { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerGroup> CustomerGroups { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<LocationGroup> LocationGroups { get; set; }
        public virtual DbSet<MyCompany> MyCompanies { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<PartCost> PartCosts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductPrice> ProductPrices { get; set; }
        public virtual DbSet<TaxRate> TaxRates { get; set; }
        public virtual DbSet<TrackingPart> TrackingParts { get; set; }
        public virtual DbSet<Uom> Uoms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroup> UsersGroups { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AddressTypeConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new CountryConfiguration());
            modelBuilder.Configurations.Add(new FobTypeConfiguration());
            modelBuilder.Configurations.Add(new LocationTypeConfiguration());
            modelBuilder.Configurations.Add(new PaymentMethodConfiguration());
            modelBuilder.Configurations.Add(new PaymentTermConfiguration());
            modelBuilder.Configurations.Add(new ProvinceConfiguration());
            modelBuilder.Configurations.Add(new ShippingTermConfiguration());
            modelBuilder.Configurations.Add(new TrackingConfiguration());
            modelBuilder.Configurations.Add(new UomTypeConfiguration());

            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new CarrierConfiguration());
            modelBuilder.Configurations.Add(new CarrierServiceConfiguration());
            modelBuilder.Configurations.Add(new CompanyConfiguration());
            modelBuilder.Configurations.Add(new ContactInformationConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new CustomerGroupConfiguration());
            modelBuilder.Configurations.Add(new LocationConfiguration());
            modelBuilder.Configurations.Add(new LocationGroupConfiguration());
            modelBuilder.Configurations.Add(new MyCompanyConfiguration());
            modelBuilder.Configurations.Add(new PartConfiguration());
            modelBuilder.Configurations.Add(new PartCostConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProductCategoryConfiguration());
            modelBuilder.Configurations.Add(new ProductPriceConfiguration());
            modelBuilder.Configurations.Add(new TaxRateConfiguration());
            modelBuilder.Configurations.Add(new TrackingPartConfiguration());
            modelBuilder.Configurations.Add(new UomConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserGroupConfiguration());
            modelBuilder.Configurations.Add(new VendorConfiguration());
        }
    }
}
