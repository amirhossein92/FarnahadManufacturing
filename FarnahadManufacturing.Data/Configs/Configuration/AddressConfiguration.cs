using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            ToTable("Address", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            HasRequired(item => item.AddressType)
                .WithMany(addressType => addressType.Addresses)
                .HasForeignKey(item => item.AddressTypeId);
            HasOptional(item => item.Country)
                .WithMany(country => country.Addresses)
                .HasForeignKey(item => item.CountryId);
            HasOptional(item => item.City)
                .WithMany(city => city.Addresses)
                .HasForeignKey(item => item.CityId);
            HasRequired(item => item.Company)
                .WithMany(company => company.Addresses)
                .HasForeignKey(item => item.CompanyId)
                .WillCascadeOnDelete(false);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Addresses)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
