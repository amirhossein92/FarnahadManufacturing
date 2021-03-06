﻿using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class ProductAssociatePriceConfiguration : EntityTypeConfiguration<ProductAssociatePrice>
    {
        public ProductAssociatePriceConfiguration()
        {
            ToTable("ProductAssociatePrice", FmDbSchema.Configuration.ToString());
            HasRequired(item => item.ProductAssociatedPriceType)
                .WithMany(productAssociatedPriceType => productAssociatedPriceType.ProductAssociatePrices)
                .HasForeignKey(item => item.ProductAssociatedPriceTypeId);
            HasRequired(item => item.Product)
                .WithMany(product => product.ProductAssociatePrices)
                .HasForeignKey(item => item.ProductId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.ProductAssociatePrices)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
