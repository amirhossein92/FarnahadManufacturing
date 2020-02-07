using System.Data.Entity.ModelConfiguration;
using System.Runtime.CompilerServices;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CarrierConfiguration : EntityTypeConfiguration<Carrier>
    {
        public CarrierConfiguration()
        {
            this.ToTable("Carrier", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.Property(item => item.Scac).HasMaxLength(4);
        }
    }
}
