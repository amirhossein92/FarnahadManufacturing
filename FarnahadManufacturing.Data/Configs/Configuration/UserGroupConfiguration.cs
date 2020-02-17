using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class UserGroupConfiguration : EntityTypeConfiguration<UserGroup>
    {
        public UserGroupConfiguration()
        {
            this.ToTable("UserGroup", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).HasMaxLength(128).IsRequired();
            this.HasMany(item => item.MembersUsers)
                .WithMany(user => user.UserGroupsMembers)
                .Map(item =>
                {
                    item.ToTable("UserUserGroup", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("UserGroupId");
                    item.MapRightKey("UserId");
                });
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.UserGroups)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
