using System.Data.Entity.ModelConfiguration;
using FarnahadManufacturing.Model.BaseConfiguration;

namespace FarnahadManufacturing.Data.Configs.BaseConfiguration
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            ToTable("Category", FmDbSchema.BaseConfiguration.ToString());
            Property(item => item.Title).IsRequired().HasMaxLength(128);
            HasRequired(item => item.CreatedByUser)
                .WithMany(user => user.Categories)
                .HasForeignKey(item => item.CreatedByUserId)
                .WillCascadeOnDelete(false);
        }
    }
}
