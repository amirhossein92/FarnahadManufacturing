using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User", FmDbSchema.Configuration.ToString());
            Property(item => item.FirstName).IsRequired().HasMaxLength(128);
            Property(item => item.LastName).IsRequired().HasMaxLength(256);
            Property(item => item.UserName).IsRequired().HasMaxLength(128);
            Property(item => item.Password).IsRequired().HasMaxLength(128);
            Property(item => item.PasswordSalt).IsRequired().HasMaxLength(128);
        }
    }
}
