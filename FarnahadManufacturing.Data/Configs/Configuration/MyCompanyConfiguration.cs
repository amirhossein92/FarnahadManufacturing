using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class MyCompanyConfiguration : EntityTypeConfiguration<MyCompany>
    {
        public MyCompanyConfiguration()
        {
            ToTable("MyCompany", FmDbSchema.Configuration.ToString());
        }
    }
}
