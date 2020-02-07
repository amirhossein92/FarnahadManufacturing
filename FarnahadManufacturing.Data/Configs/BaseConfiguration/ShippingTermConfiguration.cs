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
        }
    }
}
