using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class DocumentchargesConfiguration : IEntityTypeConfiguration<Documentcharges>
    {
        public void Configure(EntityTypeBuilder<Documentcharges> builder)
        {
            builder.ToTable("documentcharges", "lms");

            builder.HasIndex(e => e.PropertyTypeId)
                .HasName("fkdocumentproperty_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentCharge).HasColumnType("decimal(18,3)");

            builder.Property(e => e.FromDate).HasColumnType("date");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PropertyTypeId).HasColumnType("int(11)");

            builder.Property(e => e.ToDate).HasColumnType("date");

            builder.HasOne(d => d.PropertyType)
                .WithMany(p => p.Documentcharges)
                .HasForeignKey(d => d.PropertyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkdocumentproperty");



        }
    }
}
