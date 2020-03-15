using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class TrackingConfiguration : EntityTypeConfiguration<Tracking>
    {
        public TrackingConfiguration()
        {
            ToTable("Tracking", FmDbSchema.BaseConfiguration.ToString());
            Property(item => item.Title).HasMaxLength(128).IsRequired();
            Property(item => item.Abbreviation).HasMaxLength(4).IsRequired();
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Trackings)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
