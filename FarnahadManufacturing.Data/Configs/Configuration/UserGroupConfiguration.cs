using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class UserGroupConfiguration : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupConfiguration()
        {
            ToTable("UserGroup", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).HasMaxLength(128).IsRequired();
            HasMany(item => item.MembersUsers)
                .WithMany(user => user.UserGroupsMembers)
                .Map(item =>
                {
                    item.ToTable("UserUserGroup", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("UserGroupId");
                    item.MapRightKey("UserId");
                });
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.UserGroups)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
