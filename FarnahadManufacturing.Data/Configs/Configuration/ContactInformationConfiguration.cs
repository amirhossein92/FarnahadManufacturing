using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ContactInformationConfiguration : EntityTypeConfiguration<ContactInformation>
    {
        public ContactInformationConfiguration()
        {
            ToTable("ContactInformation", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            Property(item => item.Value).IsRequired().HasMaxLength(64);
            HasRequired(item => item.Address)
                .WithMany(address => address.ContactInformations)
                .HasForeignKey(item => item.AddressId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.ContactInformations)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
