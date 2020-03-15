using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            ToTable("Country", FmDbSchema.BaseConfiguration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            Property(item => item.Abbreviation).IsRequired().HasMaxLength(4);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Countries)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
