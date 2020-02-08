using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class PaymentTermConfiguration : EntityTypeConfiguration<PaymentTerm>
    {
        public PaymentTermConfiguration()
        {
            this.ToTable("PaymentTerm", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasMany(item => item.Customers)
                .WithOptional(customer => customer.DefaultPaymentTerm)
                .HasForeignKey(customer => customer.DefaultPaymentTermId);
            this.HasMany(item => item.Vendors)
                .WithOptional(vendor => vendor.DefaultPaymentTerm)
                .HasForeignKey(vendor => vendor.DefaultPaymentTermId);
        }
    }
}
