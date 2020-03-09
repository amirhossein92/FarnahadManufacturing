using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ProductCategoryConfiguration : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryConfiguration()
        {
            ToTable("ProductCategory", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).HasMaxLength(128).IsRequired();
            HasOptional(item => item.ParentProductCategory)
                .WithMany(productCategory => productCategory.ChildrenProductCategories)
                .HasForeignKey(item => item.ParentProductCategoryId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.ProductCategories)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
