using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.ToTable("User", FmDbSchema.Configuration.ToString());
            this.Property(item => item.FirstName).IsRequired().HasMaxLength(128);
            this.Property(item => item.LastName).IsRequired().HasMaxLength(256);
            this.Property(item => item.UserName).IsRequired().HasMaxLength(128);
            this.Property(item => item.Password).IsRequired().HasMaxLength(128);
            this.Property(item => item.PasswordSalt).IsRequired().HasMaxLength(128);
            this.HasMany(item => item.LocationGroup)
                .WithMany(item => item.Users)
                .Map(item =>
                {
                    item.ToTable("UserLocationGroup", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("UserId");
                    item.MapRightKey("LocationGroupId");
                });
        }
    }
}
