using System.Data.Entity.ModelConfiguration;
using System.Runtime.CompilerServices;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CarrierConfiguration : EntityTypeConfiguration<Carrier>
    {
        public CarrierConfiguration()
        {
            ToTable("Carrier", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            Property(item => item.Scac).HasMaxLength(4);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Carriers)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
