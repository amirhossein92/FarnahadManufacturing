using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class UomConfiguration : EntityTypeConfiguration<Uom>
    {
        public UomConfiguration()
        {
            ToTable("Uom", FmDbSchema.Configuration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            Property(item => item.Abbreviation).IsRequired().HasMaxLength(4);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Uoms)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
