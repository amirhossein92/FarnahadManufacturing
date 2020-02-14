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
            this.Property(item => item.Title).HasMaxLength(128).IsRequired();
            this.HasOptional(item => item.ParentProductCategory)
                .WithMany(productCategory => productCategory.ProductCategories)
                .HasForeignKey(item => item.ParentProductCategoryId);
            this.HasMany(item => item.Products)
                .WithMany(item => item.ProductCategories)
                .Map(item =>
                {
                    item.ToTable("ProductProductCategory", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("ProductCategoryId");
                    item.MapRightKey("ProductId");
                });
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.ProductCategories)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
