using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration()
        {
            ToTable("Location", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            HasRequired(item => item.LocationGroup)
                .WithMany(locationGroup => locationGroup.Locations)
                .HasForeignKey(item => item.LocationGroupId);
            HasOptional(item => item.DefaultCustomer)
                .WithMany(customer => customer.Locations)
                .HasForeignKey(item => item.DefaultCustomerId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Locations)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
