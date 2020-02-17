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
            ToTable("TaxRate", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).HasMaxLength(128).IsRequired();
            Property(item => item.Abbreviation).HasMaxLength(4).IsRequired();
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.TaxRates)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
