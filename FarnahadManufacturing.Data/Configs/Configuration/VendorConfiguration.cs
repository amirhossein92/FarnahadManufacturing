using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class VendorConfiguration : EntityTypeConfiguration<Vendor>
    {
        public VendorConfiguration()
        {
            this.ToTable("Vendor", FmDbSchema.Configuration.ToString());
        }
    }
}
