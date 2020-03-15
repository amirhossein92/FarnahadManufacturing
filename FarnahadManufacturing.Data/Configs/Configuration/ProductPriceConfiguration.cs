using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ProductPriceConfiguration : EntityTypeConfiguration<ProductPrice>
    {
        public ProductPriceConfiguration()
        {
            ToTable("ProductPrice", FmDbSchema.Configuration.ToString());
            HasRequired(item => item.Product)
                .WithMany(product => product.ProductPrices)
                .HasForeignKey(item => item.ProductId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.ProductPrices)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
