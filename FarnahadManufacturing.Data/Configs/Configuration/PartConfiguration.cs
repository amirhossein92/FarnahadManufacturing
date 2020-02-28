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
            ToTable("Part", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).HasMaxLength(128).IsRequired();
            Property(item => item.Number).HasMaxLength(64).IsRequired();
            Property(item => item.Upc).HasMaxLength(32);
            HasRequired(item => item.Uom)
                .WithMany(uom => uom.Parts)
                .HasForeignKey(item => item.UomId);
            HasOptional(item => item.DefaultLocation)
                .WithMany(location => location.Parts)
                .HasForeignKey(item => item.DefaultLocationId);
            HasOptional(item => item.DefaultVendor)
                .WithMany(vendor => vendor.Parts)
                .HasForeignKey(item => item.DefaultVendorId);
            HasRequired(item => item.DistanceUom)
                .WithMany(uom => uom.PartsDistanceUom)
                .HasForeignKey(item => item.DistanceUomId)
                .WillCascadeOnDelete(false);
            HasRequired(item => item.WeightUom)
                .WithMany(uom => uom.PartsWeightUom)
                .HasForeignKey(item => item.WeightUomId)
                .WillCascadeOnDelete(false);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Parts)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
            HasRequired(item => item.LastChangedByUser)
                .WithMany(user => user.ChangedParts)
                .HasForeignKey(item => item.LastChangedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
