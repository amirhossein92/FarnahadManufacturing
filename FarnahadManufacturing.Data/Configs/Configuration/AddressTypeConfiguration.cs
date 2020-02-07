using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class AddressTypeConfiguration : EntityTypeConfiguration<AddressType>
    {
        public AddressTypeConfiguration()
        {
            this.ToTable("AddressType", FmDbSchema.Configuration.ToString());

        }
    }
}
