using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class PartReorderInformationConfiguration : EntityTypeConfiguration<PartReorderInformation>
    {
        public PartReorderInformationConfiguration()
        {
            ToTable("PartReorderInformation", FmDbSchema.Configuration.ToString());
            HasRequired(item => item.Part)
                .WithMany(part => part.PartReorderInformations)
                .HasForeignKey(item => item.PartId);
            HasRequired(item => item.LocationGroup)
                .WithMany(locationGroup => locationGroup.PartReorderInformations)
                .HasForeignKey(item => item.LocationGroupId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.PartReorderInformations)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);

        }
    }
}
