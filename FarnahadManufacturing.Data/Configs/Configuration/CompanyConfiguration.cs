using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            ToTable("Company", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            HasOptional(item => item.DefaultCarrier)
                .WithMany(carrier => carrier.Companies)
                .HasForeignKey(item => item.DefaultCarrierId);
            HasOptional(item => item.DefaultCarrierService)
                .WithMany(carrierService => carrierService.Companies)
                .HasForeignKey(item => item.DefaultCarrierServiceId);
            HasOptional(item => item.DefaultShippingTerm)
                .WithMany(shippingTerm => shippingTerm.Companies)
                .HasForeignKey(item => item.DefaultShippingTermId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Companies)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
