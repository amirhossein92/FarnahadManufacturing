﻿using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class LocationTypeConfiguration : EntityTypeConfiguration<LocationType>
    {
        public LocationTypeConfiguration()
        {
            this.ToTable("LocationType", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasMany(item => item.Locations)
                .WithRequired(location => location.LocationType)
                .HasForeignKey(location => location.LocationTypeId);
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.LocationTypes)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
