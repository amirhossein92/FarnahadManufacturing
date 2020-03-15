using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
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
