using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class MenuactionrolemapConfiguration : IEntityTypeConfiguration<Menuactionrolemap>
    {
        public void Configure(EntityTypeBuilder<Menuactionrolemap> builder)
        {
            builder.ToTable("menuactionrolemap", "lms");

            builder.HasIndex(e => e.ActionId)
                .HasName("fk_menumap_action_id_idx");

            builder.HasIndex(e => e.MenuId)
                .HasName("fk_menumap_menu_id_idx");

            builder.HasIndex(e => e.RoleId)
                .HasName("fk_menumap_role_id_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ActionId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.MenuId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.RoleId).HasColumnType("int(11)");

            builder.HasOne(d => d.Action)
                .WithMany(p => p.Menuactionrolemap)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menumap_action_id");

            builder.HasOne(d => d.Menu)
                .WithMany(p => p.Menuactionrolemap)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menumap_menu_id");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.Menuactionrolemap)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menumap_role_id");
        }
    }
}
