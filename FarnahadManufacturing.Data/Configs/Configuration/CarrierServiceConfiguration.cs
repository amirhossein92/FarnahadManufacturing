using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CarrierServiceConfiguration : EntityTypeConfiguration<CarrierService>
    {
        public CarrierServiceConfiguration()
        {
            this.ToTable("CarrierService", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.Property(item => item.Code).IsRequired().HasMaxLength(16);
            this.HasRequired(item => item.Carrier)
                .WithMany(carrier => carrier.CarrierServices)
                .HasForeignKey(item => item.CarrierId);
            this.HasMany(item => item.Companies)
                .WithOptional(company => company.DefaultCarrierService)
                .HasForeignKey(company => company.DefaultCarrierServiceId);
        }
    }
}
