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
            this.Property(item => item.Title).HasMaxLength(128).IsRequired();
            this.HasRequired(item => item.Part)
                .WithMany(part => part.Products)
                .HasForeignKey(item => item.PartId)
                .WillCascadeOnDelete(false);
            this.HasRequired(item => item.Uom)
                .WithMany(uom => uom.Products)
                .HasForeignKey(item => item.UomId);
            this.HasOptional(item => item.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(item => item.CategoryId);
            this.Property(item => item.Upc).HasMaxLength(32);
            this.Property(item => item.Sku).HasMaxLength(32);
            this.HasRequired(item => item.DistanceUom)
                .WithMany(uom => uom.ProductsDistanceUom)
                .HasForeignKey(item => item.DistanceUomId)
                .WillCascadeOnDelete(false);
            this.HasRequired(item => item.WeightUom)
                .WithMany(uom => uom.ProductsWeightUom)
                .HasForeignKey(item => item.WeightUomId)
                .WillCascadeOnDelete(false);
            this.HasMany(item => item.ProductPrices)
                .WithRequired(productPrice => productPrice.Product)
                .HasForeignKey(productPrice => productPrice.ProductId);
            this.HasMany(item => item.ProductCategories)
                .WithMany(item => item.Products)
                .Map(item =>
                {
                    item.ToTable("ProductProductCategory", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("ProductId");
                    item.MapRightKey("ProductCategoryId");
                });
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Products)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
