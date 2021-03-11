using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class GroundrentConfiguration : IEntityTypeConfiguration<Groundrent>
    {
        public void Configure(EntityTypeBuilder<Groundrent> builder)
        {
            builder.ToTable("groundrent", "lms");

            builder.HasIndex(e => e.PropertyTypeId)
                .HasName("fkpropertytype_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FromDate).HasColumnType("date");

            builder.Property(e => e.GroundRate)
                .HasColumnName("GroundRate")
                .HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PropertyTypeId).HasColumnType("int(11)");

            builder.Property(e => e.ToDate).HasColumnType("date");

            builder.HasOne(d => d.PropertyType)
                .WithMany(p => p.Groundrate)
                .HasForeignKey(d => d.PropertyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_propertytype");
        }
        }
    }
