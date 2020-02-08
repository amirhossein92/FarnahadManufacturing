using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration()
        {
            this.ToTable("Location", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasRequired(item => item.LocationType)
                .WithMany(locationType => locationType.Locations)
                .HasForeignKey(item => item.LocationTypeId);
            this.HasRequired(item => item.LocationGroup)
                .WithMany(locationGroup => locationGroup.Locations)
                .HasForeignKey(item => item.LocationGroupId);
            this.HasOptional(item => item.DefaultCustomer)
                .WithMany(customer => customer.Locations)
                .HasForeignKey(item => item.DefaultCustomerId);
            this.HasMany(item => item.Parts)
                .WithOptional(part => part.DefaultLocation)
                .HasForeignKey(part => part.DefaultLocationId);
        }
    }
}
