using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ProductSubstituteConfiguration : EntityTypeConfiguration<ProductSubstitute>
    {
        public ProductSubstituteConfiguration()
        {
            this.ToTable("ProductSubstitute", FmDbSchema.Configuration.ToString());
            HasRequired(item => item.Product)
                .WithMany(product => product.ProductSubstitutes)
                .HasForeignKey(item => item.ProductId);
            HasRequired(item => item.SubstituteProduct)
                .WithMany(product => product.ProductProductSubstitutes)
                .HasForeignKey(item => item.SubstituteProductId)
                .WillCascadeOnDelete(false);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.ProductSubstitutes)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
