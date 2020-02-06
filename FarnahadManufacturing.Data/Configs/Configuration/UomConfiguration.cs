using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class UomConfiguration : EntityTypeConfiguration<Uom>
    {
        public UomConfiguration()
        {
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.Property(item => item.Abbreviation).HasMaxLength(4);
        }
    }
}
