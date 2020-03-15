using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
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
