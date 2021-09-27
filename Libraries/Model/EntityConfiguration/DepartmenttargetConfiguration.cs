using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class DepartmenttargetConfiguration : IEntityTypeConfiguration<Departmenttarget>
    {
        public void Configure(EntityTypeBuilder<Departmenttarget> builder)
        {
            //builder.ToTable("departmenttarget");

            builder.HasIndex(e => e.DepartmentId)
                   .HasName("DepartmenttargetDepartmentId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.FilesToBeDone).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.WeeklyToBeDone).HasColumnType("int(11)");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Departmenttarget)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DepartmenttargetDepartmentId");
        }
    }
}