using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.Configuration;

// CHECK
namespace FarnahadManufacturing.Data.Configs.Configuration
{
    public class PartCostConfiguration : EntityTypeConfiguration<PartCost>
    {
        public PartCostConfiguration()
        {
            ToTable("PartCost", FmDbSchema.Configuration.ToString());
            HasRequired(item => item.Part)
                .WithMany(part => part.PartCosts)
                .HasForeignKey(item => item.PartId);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.PartCosts)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
