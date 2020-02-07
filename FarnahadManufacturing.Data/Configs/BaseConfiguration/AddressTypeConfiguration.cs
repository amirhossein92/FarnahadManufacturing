using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class AddressTypeConfiguration : EntityTypeConfiguration<AddressType>
    {
        public AddressTypeConfiguration()
        {
            this.ToTable("AddressType", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
        }
    }
}
