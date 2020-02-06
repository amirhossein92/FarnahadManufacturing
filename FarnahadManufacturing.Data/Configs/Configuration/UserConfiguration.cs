using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.Property(item => item.FirstName).IsRequired().HasMaxLength(128);
            this.Property(item => item.FirstName).IsRequired().HasMaxLength(128);
            this.Property(item => item.UserName).IsRequired().HasMaxLength(128);
            this.Property(item => item.Password).IsRequired().HasMaxLength(128);
            this.Property(item => item.PasswordSalt).IsRequired().HasMaxLength(128);
        }
    }
}
