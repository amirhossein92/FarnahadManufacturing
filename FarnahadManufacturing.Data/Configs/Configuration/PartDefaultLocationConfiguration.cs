﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class PartDefaultLocationConfiguration : EntityTypeConfiguration<PartDefaultLocation>
    {
        public PartDefaultLocationConfiguration()
        {
            ToTable("PartDefaultLocation", FmDbSchema.Configuration.ToString());
            HasRequired(item => item.Part)
                .WithMany(part => part.PartDefaultLocations)
                .HasForeignKey(item => item.PartId);
            HasRequired(item => item.LocationGroup)
                .WithMany(locationGroup => locationGroup.PartDefaultLocations)
                .HasForeignKey(item => item.LocationGroupId);
            HasOptional(item => item.DefaultLocation)
                .WithMany(location => location.PartDefaultLocations)
                .HasForeignKey(item => item.DefaultLocationId);
        }
    }
}
