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
        public void Configure(EntityTypeBuilder<PageRole> builder)
        {
            builder.ToTable("pagerole");

            builder.HasIndex(e => e.PageId)
                .HasName("fk_PageID_idx");

            builder.HasIndex(e => e.RoleId)
                .HasName("fk_RoleName_idx");

            builder.HasIndex(e => e.UserId)
                .HasName("fk_User_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy)
                .HasColumnName("Created By")
                .HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnName("Created Date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy)
                .HasColumnName("Modified By")
                .HasColumnType("int(11)");

            builder.Property(e => e.ModuleId)
                .HasColumnName("ModuleID")
                .HasColumnType("int(11)");

            builder.Property(e => e.PageId)
                .HasColumnName("PageID")
                .HasColumnType("int(11)");

            builder.Property(e => e.RAdd)
                .HasColumnName("R_Add")
                .HasColumnType("tinyint(4)");

            builder.Property(e => e.RDelete)
                .HasColumnName("R_Delete")
                .HasColumnType("tinyint(4)");

            builder.Property(e => e.REdit)
                .HasColumnName("R_Edit")
                .HasColumnType("tinyint(4)");

            builder.Property(e => e.RView)
                .HasColumnName("R_View")
                .HasColumnType("tinyint(4)");
            
            builder.Property(e => e.RView)
                .HasColumnName("R_View")
                .HasColumnType("tinyint(4)");

            builder.Property(e => e.RoleId)
                .HasColumnName("RoleID")
                .HasColumnType("int(11)");

            builder.Property(e => e.UserId)
                .HasColumnName("UserID")
                .HasColumnType("int(11)");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.PageRole)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoleName");

            builder.HasOne(d => d.User)
                .WithMany(p => p.PageRole)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_User");

        }
    }
}
