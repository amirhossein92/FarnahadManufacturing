using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class CustomerGroupConfiguration : EntityTypeConfiguration<CustomerGroup>
    {
        public CustomerGroupConfiguration()
        {
            this.ToTable("CustomerGroup", FmDbSchema.Configuration.ToString());
            this.Property(item => item.Title).HasMaxLength(128).IsRequired();
            this.HasMany(item => item.Customers)
                .WithMany(customerGroup => customerGroup.CustomerGroups)
                .Map(item =>
                {
                    item.ToTable("CustomerCustomerGroup", FmDbSchema.Configuration.ToString());
                    item.MapLeftKey("CustomerGroupId");
                    item.MapRightKey("CustomerId");
                });
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.CustomerGroups)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
