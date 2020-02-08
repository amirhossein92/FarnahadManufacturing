using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ContactInformationConfiguration : EntityTypeConfiguration<ContactInformation>
    {
        public ContactInformationConfiguration()
        {
            this.ToTable("ContactInformation", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.Property(item => item.Value).IsRequired().HasMaxLength(64);
            this.HasRequired(item => item.Address)
                .WithMany(address => address.ContactInformations)
                .HasForeignKey(item => item.AddressId);
        }
    }
}
