using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class LocalityConfiguration:IEntityTypeConfiguration<Locality>
    {
        public void Configure(EntityTypeBuilder<Locality> builder)
        {
            builder.ToTable("locality");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(600)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");
            builder.Property(e => e.DivisionId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'1'");

            builder.Property(e => e.Landmark)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LocalityCode)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            builder.Property(e => e.ZoneId).HasColumnType("int(11)");
            builder.Property(e => e.DivisionId).HasColumnType("int(11)");
        }
    }
}
