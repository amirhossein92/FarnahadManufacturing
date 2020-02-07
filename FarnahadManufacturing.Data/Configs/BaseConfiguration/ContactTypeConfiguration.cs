using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class ContactTypeConfiguration : EntityTypeConfiguration<ContactType>
    {
        public ContactTypeConfiguration()
        {
            this.ToTable("ContactType", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
        }
    }
}
