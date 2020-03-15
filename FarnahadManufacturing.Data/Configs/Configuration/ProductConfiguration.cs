using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Product", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).HasMaxLength(128).IsRequired();
            HasRequired(item => item.Part)
                .WithMany(part => part.Products)
                .HasForeignKey(item => item.PartId)
                .WillCascadeOnDelete(false);
            HasRequired(item => item.Uom)
                .WithMany(uom => uom.Products)
                .HasForeignKey(item => item.UomId);
            HasOptional(item => item.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(item => item.CategoryId);
            Property(item => item.Upc).HasMaxLength(32);
            Property(item => item.Sku).HasMaxLength(32);
            HasRequired(item => item.DistanceUom)
                .WithMany(uom => uom.ProductsDistanceUom)
                .HasForeignKey(item => item.DistanceUomId)
                .WillCascadeOnDelete(false);
            HasRequired(item => item.WeightUom)
                .WithMany(uom => uom.ProductsWeightUom)
                .HasForeignKey(item => item.WeightUomId)
                .WillCascadeOnDelete(false);
            HasMany(item => item.ProductCategories)
                .WithMany(item => item.Products)
                .Map(item =>
                {
                    item.ToTable("ProductProductCategory", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("ProductId");
                    item.MapRightKey("ProductCategoryId");
                });
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Products)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
