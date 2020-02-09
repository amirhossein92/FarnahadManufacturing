using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            this.ToTable("Customer", FmDbSchema.Configuration.ToString());
            this.HasOptional(item => item.DefaultPaymentTerm)
                .WithMany(paymentTerm => paymentTerm.Customers)
                .HasForeignKey(item => item.DefaultPaymentTermId);
            this.HasOptional(item => item.Salesperson)
                .WithMany(user => user.CustomerSalespersons)
                .HasForeignKey(item => item.SalespersonId);
            this.HasOptional(item => item.Category)
                .WithMany(user => user.Customers)
                .HasForeignKey(item => item.CategoryId);
            this.Property(item => item.AccountNumber).HasMaxLength(64);
            this.HasRequired(item => item.TaxRate)
                .WithMany(taxRate => taxRate.Customers)
                .HasForeignKey(item => item.TaxRateId);
            this.Property(item => item.TaxExemptNumber).HasMaxLength(64);
            this.HasMany(item => item.CustomerGroups)
                .WithMany(customerGroup => customerGroup.Customers)
                .Map(item =>
                {
                    item.ToTable("CustomerCustomerGroup", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("CustomerId");
                    item.MapRightKey("CustomerGroupId");
                });
            this.HasMany(item => item.Locations)
                .WithOptional(location => location.DefaultCustomer)
                .HasForeignKey(location => location.DefaultCustomerId);
        }
    }
}
