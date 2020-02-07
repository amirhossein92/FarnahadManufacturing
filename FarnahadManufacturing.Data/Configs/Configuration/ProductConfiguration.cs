using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            this.ToTable("Product", FmDbSchema.Configuration.ToString());
            this.HasMany(item => item.ProductCategories)
                .WithMany(item => item.Products)
                .Map(item =>
                {
                    item.ToTable("ProductProductCategory", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("ProductId");
                    item.MapRightKey("ProductCategoryId");
                });
        }
    }
}
