using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class TrackingPartConfiguration : EntityTypeConfiguration<TrackingPart>
    {
        public TrackingPartConfiguration()
        {
            this.ToTable("TrackingPart", FmDbSchema.Configuration.ToString());
            // TODO: Config many to many relation
        }
    }
}
