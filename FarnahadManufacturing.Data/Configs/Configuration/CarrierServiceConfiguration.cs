﻿using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CarrierServiceConfiguration : EntityTypeConfiguration<CarrierService>
    {
        public CarrierServiceConfiguration()
        {
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.Property(item => item.Code).IsRequired().HasMaxLength(16);
        }
    }
}
