using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ProductCategoryConfiguration : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfiguration()
        {
            this.ToTable("ProductCategory", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
        }
    }
}
