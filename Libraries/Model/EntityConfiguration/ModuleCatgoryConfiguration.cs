using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class ModuleCatgoryConfiguration : IEntityTypeConfiguration<ModuleCategory>
    {
        public void Configure(EntityTypeBuilder<ModuleCategory> builder)
        {
            builder.ToTable("modulecategory", "lms");

            builder.Property(e => e.Id)
                  .HasColumnName("id")
                  .HasColumnType("int(11)");

            builder.Property(e => e.CategoryName)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

        }
    }
}
