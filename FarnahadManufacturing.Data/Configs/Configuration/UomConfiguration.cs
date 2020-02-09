using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class UomConfiguration : EntityTypeConfiguration<Uom>
    {
        public UomConfiguration()
        {
            this.ToTable("Uom", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.Property(item => item.Abbreviation).HasMaxLength(4);
            this.HasRequired(item => item.UomType)
                .WithMany(uomType => uomType.Uoms)
                .HasForeignKey(item => item.UomTypeId);
            // TODO: How to configure other Uoms: DistanceUOM
            this.HasMany(item => item.Parts)
                .WithRequired(part => part.Uom)
                .HasForeignKey(part => part.UomId);
            this.HasMany(item => item.PartsDistanceUom)
                .WithRequired(part => part.DistanceUom)
                .HasForeignKey(part => part.DistanceUomId);
            this.HasMany(item => item.PartsWeightUom)
                .WithRequired(part => part.WeightUom)
                .HasForeignKey(part => part.WeightUomId);
            this.HasMany(item => item.Products)
                .WithRequired(product => product.Uom)
                .HasForeignKey(product => product.UomId);
            this.HasMany(item => item.ProductsDistanceUom)
                .WithRequired(product => product.DistanceUom)
                .HasForeignKey(product => product.DistanceUomId);
            this.HasMany(item => item.ProductsWeightUom)
                .WithRequired(product => product.WeightUom)
                .HasForeignKey(product => product.WeightUomId);
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Uoms)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
