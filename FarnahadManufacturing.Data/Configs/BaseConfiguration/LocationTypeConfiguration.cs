using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class LocationTypeConfiguration : EntityTypeConfiguration<LocationType>
    {
        public LocationTypeConfiguration()
        {
            this.ToTable("LocationType", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
        }
    }
}
