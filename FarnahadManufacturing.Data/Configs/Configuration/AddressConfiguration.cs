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
        }
    }
}
