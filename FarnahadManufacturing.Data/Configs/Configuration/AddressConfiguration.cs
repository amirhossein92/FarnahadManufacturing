using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            this.ToTable("Address", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasRequired(item => item.AddressType)
                .WithMany(addressType => addressType.Addresses)
                .HasForeignKey(item => item.AddressTypeId);
            this.HasOptional(item => item.Country)
                .WithMany(country => country.Addresses)
                .HasForeignKey(item => item.CountryId);
            this.HasOptional(item => item.City)
                .WithMany(city => city.Addresses)
                .HasForeignKey(item => item.CityId);
            this.HasMany(item => item.ContactInformations)
                .WithRequired(contactInformation => contactInformation.Address)
                .HasForeignKey(contactInformation => contactInformation.AddressId);
            // TODO: What to do about default address
            //this.HasMany(item => item.CompaniesDefaultAddress)
            //    .WithOptional(company => company.DefaultAddress)
            //    .HasForeignKey(company => company.DefaultAddressId);
            // TODO: This makes error
            //this.HasMany(item => item.Companies)
            //    .WithMany(address => address.Addresses)
            //    .Map(item =>
            //    {
            //        item.ToTable("CompanyAddress", FmDbSchema.Configuration.ToString());
            //        item.MapLeftKey("AddressId");
            //        item.MapRightKey("CompanyId");
            //    });
        }
    }
}
