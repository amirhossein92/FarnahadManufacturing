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
            this.HasMany(item => item.CarrierServices)
                .WithRequired(carrierService => carrierService.Carrier)
                .HasForeignKey(carrierService => carrierService.CarrierId);
            this.HasMany(item => item.Companies)
                .WithOptional(company => company.DefaultCarrier)
                .HasForeignKey(company => company.DefaultCarrierId);
        }
    }
}
