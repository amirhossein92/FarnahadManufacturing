﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class ProvinceConfiguration : EntityTypeConfiguration<Province>
    {
        public ProvinceConfiguration()
        {
            ToTable("Province", FmDbSchema.BaseConfiguration.ToString());
            Property(item => item.Title).HasMaxLength(128).IsRequired();
            Property(item => item.Abbreviation).HasMaxLength(4).IsRequired();
            HasRequired(item => item.Country)
                .WithMany(country => country.Provinces)
                .HasForeignKey(item => item.CountryId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Provinces)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}