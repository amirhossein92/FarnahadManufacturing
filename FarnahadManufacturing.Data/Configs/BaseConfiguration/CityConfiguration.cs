using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            this.ToTable("City", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasRequired(item => item.Country)
                .WithMany(country => country.Cities)
                .HasForeignKey(item => item.CountryId)
                .WillCascadeOnDelete(false);
            this.HasMany(item => item.Addresses)
                .WithOptional(address => address.City)
                .HasForeignKey(address => address.CityId);
        }
    }
}
