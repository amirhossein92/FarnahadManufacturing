using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class LocationGroupConfiguration : EntityTypeConfiguration<LocationGroup>
    {
        public LocationGroupConfiguration()
        {
            this.ToTable("LocationGroup", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasOptional(item => item.Category)
                .WithMany(category => category.LocationGroups)
                .HasForeignKey(item => item.CategoryId);
            this.HasMany(item => item.Users)
                .WithMany(user => user.LocationGroupMembers)
                .Map(item =>
                {
                    item.ToTable("UserLocationGroup", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("LocationGroupId");
                    item.MapRightKey("UserId");
                });
            this.HasMany(item => item.Locations)
                .WithRequired(location => location.LocationGroup)
                .HasForeignKey(location => location.LocationGroupId);
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.LocationGroups)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
