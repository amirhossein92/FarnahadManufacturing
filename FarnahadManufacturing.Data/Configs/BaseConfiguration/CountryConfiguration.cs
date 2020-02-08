using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            this.ToTable("Country", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasMany(item => item.Cities)
                .WithRequired(city => city.Country)
                .HasForeignKey(city => city.CountryId);
            this.HasMany(item => item.Addresses)
                .WithOptional(address => address.Country)
                .HasForeignKey(address => address.CountryId);
        }
    }
}
