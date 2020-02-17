using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class FobTypeConfiguration : EntityTypeConfiguration<FobType>
    {
        public FobTypeConfiguration()
        {
            ToTable("FobType", FmDbSchema.BaseConfiguration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.FobTypes)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
