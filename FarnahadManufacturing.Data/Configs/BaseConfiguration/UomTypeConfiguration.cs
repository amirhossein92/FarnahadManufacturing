using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class UomTypeConfiguration : EntityTypeConfiguration<UomType>
    {
        public UomTypeConfiguration()
        {
            this.ToTable("UomType", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
        }
    }
}
