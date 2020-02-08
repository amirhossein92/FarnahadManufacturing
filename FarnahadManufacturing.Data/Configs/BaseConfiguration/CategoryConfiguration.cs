using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            this.ToTable("Category", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            // TODO: one to one relationship config
            this.HasMany(item => item.Customers)
                .WithOptional(customer => customer.Category)
                .HasForeignKey(customer => customer.CategoryId);
            this.HasMany(item => item.LocationGroups)
                .WithOptional(locationGroups => locationGroups.Category)
                .HasForeignKey(locationGroups => locationGroups.CategoryId);
            this.HasMany(item => item.Products)
                .WithOptional(product => product.Category)
                .HasForeignKey(product => product.CategoryId);
        }
    }
}
