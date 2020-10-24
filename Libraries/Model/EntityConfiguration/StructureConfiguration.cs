using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class StructureConfiguration:IEntityTypeConfiguration<Structure>
    {
        public void Configure(EntityTypeBuilder<Structure> entity)
        {
            entity.ToTable("structure", "lms");

            entity.HasIndex(e => e.Name)
                .HasName("StructureName_UNIQUE")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
        }
    }
}