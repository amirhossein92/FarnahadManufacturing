using System;
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
            this.Property(item => item.Title).HasMaxLength(128).IsRequired();
            this.Property(item => item.Abbreviation).HasMaxLength(4).IsRequired();
            this.HasMany(item => item.Customers)
                .WithRequired(customer => customer.TaxRate)
                .HasForeignKey(customer => customer.TaxRateId);

        }
    }
}
