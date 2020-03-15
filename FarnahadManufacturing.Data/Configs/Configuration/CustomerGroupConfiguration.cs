using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CustomerGroupConfiguration : EntityTypeConfiguration<CustomerGroup>
    {
        public CustomerGroupConfiguration()
        {
            ToTable("CustomerGroup", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).HasMaxLength(128).IsRequired();
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.CustomerGroups)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
