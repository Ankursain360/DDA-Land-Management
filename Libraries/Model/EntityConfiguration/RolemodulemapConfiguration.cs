using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class RolemodulemapConfiguration : IEntityTypeConfiguration<Rolemodulemap>
    {
        public void Configure(EntityTypeBuilder<Rolemodulemap> builder)
        {
            builder.ToTable("rolemodulemap", "lms");

            builder.HasIndex(e => e.ModuleId)
                .HasName("fk_module_id_idx");

            builder.HasIndex(e => e.RoleId)
                .HasName("fk_role_id_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.ModuleId).HasColumnType("int(11)");

            builder.Property(e => e.RoleId).HasColumnType("int(11)");

            builder.HasOne(d => d.Module)
                .WithMany(p => p.Rolemodulemap)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_module_id");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.Rolemodulemap)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_role_id");
        }
    }
}
