using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class UomTypeConfiguration : EntityTypeConfiguration<UomType>
    {
        public UomTypeConfiguration()
        {
            ToTable("UomType", FmDbSchema.BaseConfiguration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.UomTypes)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
