
using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class DivisionConfiguration : IEntityTypeConfiguration<Division>
    {

        public void Configure(EntityTypeBuilder<Division> builder)
        {
            builder.ToTable("division", "lms");

            builder.HasIndex(e => e.DepartmentId)
                .HasName("fkDepartmentid1_idx");

            builder.HasIndex(e => e.Name)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.ZoneId)
                .HasName("ZoneId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Division)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkDepartmentID");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Division)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkZoneid1");

        }


    }
}
