using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class UomTypeConfiguration : EntityTypeConfiguration<UomType>
    {
        public UomTypeConfiguration()
        {
            this.ToTable("UomType", FmDbSchema.BaseConfiguration.ToString());
            this.Property(item => item.Title).IsRequired().HasMaxLength(128);
            this.HasMany(item => item.Uoms)
                .WithRequired(uom => uom.UomType)
                .HasForeignKey(uom => uom.UomTypeId);
            this.HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.UomTypes)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
