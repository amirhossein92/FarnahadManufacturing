using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class LocationGroupConfiguration : EntityTypeConfiguration<LocationGroup>
    {
        public LocationGroupConfiguration()
        {
            ToTable("LocationGroup", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            HasOptional(item => item.Category)
                .WithMany(category => category.LocationGroups)
                .HasForeignKey(item => item.CategoryId);
            HasMany(item => item.Users)
                .WithMany(user => user.LocationGroupMembers)
                .Map(item =>
                {
                    item.ToTable("UserLocationGroup", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("LocationGroupId");
                    item.MapRightKey("UserId");
                });
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.LocationGroups)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
