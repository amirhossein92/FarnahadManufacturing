using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class LocationTypeConfiguration : EntityTypeConfiguration<LocationType>
    {
        public LocationTypeConfiguration()
        {
            ToTable("LocationType", FmDbSchema.BaseConfiguration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.LocationTypes)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
