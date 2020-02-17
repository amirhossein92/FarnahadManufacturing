using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class PaymentMethodConfiguration : EntityTypeConfiguration<PaymentMethod>
    {
        public PaymentMethodConfiguration()
        {
            ToTable("PaymentMethod", FmDbSchema.BaseConfiguration.ToString());
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.PaymentMethods)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
