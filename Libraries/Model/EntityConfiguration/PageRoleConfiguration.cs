using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class PageRoleConfiguration : IEntityTypeConfiguration<PageRole>
    {
        public void Configure(EntityTypeBuilder<PageRole> entity)
        {
            entity.ToTable("pagerole");

            entity.HasIndex(e => e.PageId)
                .HasName("fk_PageID_idx");

            entity.HasIndex(e => e.RoleId)
                .HasName("fk_RoleName_idx");

            entity.HasIndex(e => e.UserId)
                .HasName("fk_User_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.ModuleId)
                .HasColumnName("ModuleID")
                .HasColumnType("int(11)");

            entity.Property(e => e.PageId)
                .HasColumnName("PageID")
                .HasColumnType("int(11)");

            entity.Property(e => e.RAdd)
                .HasColumnName("R_Add")
                .HasColumnType("tinyint(4)");

            entity.Property(e => e.RDelete)
                .HasColumnName("R_Delete")
                .HasColumnType("tinyint(4)");

            entity.Property(e => e.RDisplay)
                .HasColumnName("R_Display")
                .HasColumnType("tinyint(4)");

            entity.Property(e => e.REdit)
                .HasColumnName("R_Edit")
                .HasColumnType("tinyint(4)");

            entity.Property(e => e.RView)
                .HasColumnName("R_View")
                .HasColumnType("tinyint(4)");

            entity.Property(e => e.RoleId)
                .HasColumnName("RoleID")
                .HasColumnType("int(11)");

            entity.Property(e => e.UserId)
                .HasColumnName("UserID")
                .HasColumnType("int(11)");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.PageRole)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoleName");

            entity.HasOne(d => d.User)
                .WithMany(p => p.PageRole)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_User");
        }
    }
}
