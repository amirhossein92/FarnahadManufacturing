using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CarrierServiceConfiguration : EntityTypeConfiguration<CarrierService>
    {
        public CarrierServiceConfiguration()
        {
            ToTable("CarrierService", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            Property(item => item.Code).IsRequired().HasMaxLength(16);
            HasRequired(item => item.Carrier)
                .WithMany(carrier => carrier.CarrierServices)
                .HasForeignKey(item => item.CarrierId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.CarrierServices)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
