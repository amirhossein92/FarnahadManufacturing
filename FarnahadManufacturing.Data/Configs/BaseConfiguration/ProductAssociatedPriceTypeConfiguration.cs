using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class ProductAssociatedPriceTypeConfiguration : EntityTypeConfiguration<ProductAssociatedPriceType>
    {
        public ProductAssociatedPriceTypeConfiguration()
        {
            ToTable("ProductAssociatedPriceType", FmDbSchema.BaseConfiguration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.ProductAssociatedPriceTypes)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
