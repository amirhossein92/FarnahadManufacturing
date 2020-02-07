using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class LocationGroupConfiguration : EntityTypeConfiguration<LocationGroup>
    {
        public LocationGroupConfiguration()
        {
            this.ToTable("LocationGroup", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
        }
    }
}
