﻿using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class TrackingPartConfiguration : EntityTypeConfiguration<TrackingPart>
    {
        public TrackingPartConfiguration()
        {
            ToTable("TrackingPart", FmDbSchema.Configuration.ToString());
            HasRequired(item => item.Tracking)
                .WithMany(tracking => tracking.TrackingParts)
                .HasForeignKey(item => item.TrackingId);
            HasRequired(item => item.Part)
                .WithMany(part => part.TrackingParts)
                .HasForeignKey(item => item.PartId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.TrackingParts)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
