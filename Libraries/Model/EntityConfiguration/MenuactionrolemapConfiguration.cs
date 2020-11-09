using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class MenuactionrolemapConfiguration : IEntityTypeConfiguration<Menuactionrolemap>
    {
        public void Configure(EntityTypeBuilder<Menuactionrolemap> builder)
        {
            builder.ToTable("submenuactionrolemap", "lms");

            builder.HasIndex(e => e.ActionId)
                .HasName("fk_menumap_action_id_idx");

            builder.HasIndex(e => e.RoleId)
                .HasName("fk_menumap_role_id_idx");

            builder.HasIndex(e => e.SubMenuId)
                .HasName("fk_menumap_submenu_id_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ActionId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.RoleId).HasColumnType("int(11)");

            builder.Property(e => e.SubMenuId).HasColumnType("int(11)");

            builder.HasOne(d => d.Action)
                .WithMany(p => p.Menuactionrolemap)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menumap_action_id");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.Menuactionrolemap)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menumap_role_id");

            builder.HasOne(d => d.SubMenu)
                .WithMany(p => p.Menuactionrolemap)
                .HasForeignKey(d => d.SubMenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_menumap_submenu_id");
        }
    }
}
