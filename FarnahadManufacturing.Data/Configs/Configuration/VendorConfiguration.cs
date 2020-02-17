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
            ToTable("Vendor", FmDbSchema.Configuration.ToString());
            Property(item => item.AccountNumber).HasMaxLength(64);
            HasOptional(item => item.DefaultPaymentTerm)
                .WithMany(paymentTerm => paymentTerm.Vendors)
                .HasForeignKey(item => item.DefaultPaymentTermId);
        }
    }
}
