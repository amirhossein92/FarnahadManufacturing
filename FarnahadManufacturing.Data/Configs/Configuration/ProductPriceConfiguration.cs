using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ProductPriceConfiguration : EntityTypeConfiguration<ProductPrice>
    {
        public ProductPriceConfiguration()
        {
            this.ToTable("ProductPrice", FmDbSchema.Configuration.ToString());
            this.HasRequired(item => item.Product)
                .WithMany(product => product.ProductPrices)
                .HasForeignKey(item => item.ProductId);
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.ProductPrices)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
