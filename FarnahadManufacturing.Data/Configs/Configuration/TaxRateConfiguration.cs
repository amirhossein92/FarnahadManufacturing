﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class TaxRateConfiguration : EntityTypeConfiguration<TaxRate>
    {
        public TaxRateConfiguration()
        {
            this.ToTable("TaxRate", FmDbSchema.Configuration.ToString());
        }
    }
}
