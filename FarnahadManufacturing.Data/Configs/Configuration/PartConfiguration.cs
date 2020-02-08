using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class PartConfiguration : EntityTypeConfiguration<Part>
    {
        public PartConfiguration()
        {
            this.ToTable("Part", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).HasMaxLength(128).IsRequired();
            this.Property(item => item.Number).HasMaxLength(64).IsRequired();
            this.Property(item => item.Upc).HasMaxLength(32).IsRequired();
            this.HasRequired(item => item.Uom)
                .WithMany(uom => uom.Parts)
                .HasForeignKey(item => item.UomId);
            this.HasMany(item => item.TrackingParts)
                .WithRequired(trackingPart => trackingPart.Part)
                .HasForeignKey(trackingPart => trackingPart.PartId);
            this.HasOptional(item => item.DefaultLocation)
                .WithMany(location => location.Parts)
                .HasForeignKey(item => item.DefaultLocationId);
            this.HasOptional(item => item.DefaultVendor)
                .WithMany(vendor => vendor.Parts)
                .HasForeignKey(item => item.DefaultVendorId);
            // TODO: Check this with Mr. Ghaderian
            this.HasRequired(item => item.DistanceUom)
                .WithMany(uom => uom.PartsDistanceUom)
                .HasForeignKey(item => item.DistanceUomId);
            this.HasRequired(item => item.WeightUom)
                .WithMany(uom => uom.PartsWeightUom)
                .HasForeignKey(item => item.WeightUomId);
            this.HasMany(item => item.Products)
                .WithRequired(product => product.Part)
                .HasForeignKey(product => product.PartId);
        }
    }
}
