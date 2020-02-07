using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class TrackingConfiguration : EntityTypeConfiguration<Tracking>
    {
        public TrackingConfiguration()
        {
            this.ToTable("Tracking", FmDbSchema.BaseConfiguration.ToString());
        }
    }
}
