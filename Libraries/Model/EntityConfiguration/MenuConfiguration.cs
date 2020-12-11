using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("menu", "lms");

            builder.HasIndex(e => e.ModuleId)
                .HasName("fk_module_id_idx");

            builder.HasIndex(e => e.ParentMenuId)
                .HasName("fk_parentmenuid_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.ModuleId).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Url)
                   .HasMaxLength(200)
                   .IsUnicode(false);

            builder.Property(e => e.ParentMenuId).HasColumnType("int(11)");

            builder.Property(e => e.SortBy).HasColumnType("int(11)");

            builder.HasOne(d => d.Module)
                .WithMany(p => p.Menu)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("fk_ModuleId");

            builder.HasOne(d => d.ParentMenu)
                .WithMany(p => p.InverseParentMenu)
                .HasForeignKey(d => d.ParentMenuId)
                .HasConstraintName("fk_parentmenuid");

        }
    }
}
