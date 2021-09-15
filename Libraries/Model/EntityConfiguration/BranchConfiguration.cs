using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Libraries.Model.EntityConfiguration
{
    class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("branch", "lms");

            builder.HasIndex(e => e.DepartmentId)
                .HasName("fk_branchdept_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");
            builder.HasIndex(e => e.PropertytypeId)
               .HasName("fk_propertytype_idx");

            builder.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Branch)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("fk_branchdept");

            builder.HasOne(d => d.Propertytype)
                .WithMany(p => p.Branch)
                .HasForeignKey(d => d.PropertytypeId)
                .HasConstraintName("fk_propertytype");
        }
    }
}
