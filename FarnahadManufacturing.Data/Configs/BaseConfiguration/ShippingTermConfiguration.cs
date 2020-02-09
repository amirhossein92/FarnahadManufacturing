using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class ShippingTermConfiguration : EntityTypeConfiguration<ShippingTerm>
    {
        public ShippingTermConfiguration()
        {
            this.ToTable("ShippingTerm", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasMany(item => item.Companies)
                .WithOptional(company => company.DefaultShippingTerm)
                .HasForeignKey(company => company.DefaultShippingTermId);
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.ShippingTerms)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
