using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            this.ToTable("Company", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasOptional(item => item.DefaultCarrier)
                .WithMany(carrier => carrier.Companies)
                .HasForeignKey(item => item.DefaultCarrierId);
            this.HasOptional(item => item.DefaultCarrierService)
                .WithMany(carrierService => carrierService.Companies)
                .HasForeignKey(item => item.DefaultCarrierServiceId);
            this.HasOptional(item => item.DefaultShippingTerm)
                .WithMany(shippingTerm => shippingTerm.Companies)
                .HasForeignKey(item => item.DefaultShippingTermId);
            this.HasOptional(item => item.DefaultAddress)
                .WithOptionalDependent(address => address.CompanyDefaultAddress);
            this.HasMany(item => item.Addresses)
                .WithRequired(address => address.Company)
                .HasForeignKey(address => address.CompanyId);
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Companies)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
