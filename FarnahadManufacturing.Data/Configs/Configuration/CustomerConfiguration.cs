using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer", FmDbSchema.Configuration.ToString());
            HasOptional(item => item.DefaultPaymentTerm)
                .WithMany(paymentTerm => paymentTerm.Customers)
                .HasForeignKey(item => item.DefaultPaymentTermId);
            HasOptional(item => item.Salesperson)
                .WithMany(user => user.CustomerSalespersons)
                .HasForeignKey(item => item.SalespersonId);
            HasOptional(item => item.Category)
                .WithMany(user => user.Customers)
                .HasForeignKey(item => item.CategoryId);
            Property(item => item.AccountNumber).HasMaxLength(64);
            HasRequired(item => item.TaxRate)
                .WithMany(taxRate => taxRate.Customers)
                .HasForeignKey(item => item.TaxRateId);
            Property(item => item.TaxExemptNumber).HasMaxLength(64);
            HasMany(item => item.CustomerGroups)
                .WithMany(customerGroup => customerGroup.Customers)
                .Map(item =>
                {
                    item.ToTable("CustomerCustomerGroup", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("CustomerId");
                    item.MapRightKey("CustomerGroupId");
                });
        }
    }
}
