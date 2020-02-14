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
            this.HasRequired(item => item.Company)
                .WithMany(company => company.Addresses)
                .HasForeignKey(item => item.CompanyId)
                .WillCascadeOnDelete(false);
            this.HasMany(item => item.ContactInformations)
                .WithRequired(contactInformation => contactInformation.Address)
                .HasForeignKey(contactInformation => contactInformation.AddressId);
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Addresses)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
