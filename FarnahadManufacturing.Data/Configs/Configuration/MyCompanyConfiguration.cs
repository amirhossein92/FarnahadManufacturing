using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarnahadManufacturing.Model.Configuration;

namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class MyCompanyConfiguration : EntityTypeConfiguration<MyCompany>
    {
        public MyCompanyConfiguration()
        {
            this.ToTable("MyCompany", FmDbSchema.Configuration.ToString());
        }
    }
}
