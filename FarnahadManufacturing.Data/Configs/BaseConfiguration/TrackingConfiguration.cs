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
            this.Property(item => item.Title).HasMaxLength(128).IsRequired();
            this.Property(item => item.Abbreviation).HasMaxLength(4).IsRequired();
            this.HasMany(item => item.TrackingParts)
                .WithRequired(trackingPart => trackingPart.Tracking)
                .HasForeignKey(trackingPart => trackingPart.TrackingId);
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Trackings)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
