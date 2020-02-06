using System.Data.Entity.ModelConfiguration;
using System.Runtime.CompilerServices;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CarrierConfiguration : EntityTypeConfiguration<Carrier>
    {
        public CarrierConfiguration()
        {
            this.Property(item => item.Scac).HasMaxLength(4);
        }
    }
}
