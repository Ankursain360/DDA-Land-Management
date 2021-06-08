using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
   public class Undersection17Configuration : IEntityTypeConfiguration<Undersection17>
    {
        public void Configure(EntityTypeBuilder<Undersection17> builder)
        {
            builder.ToTable("undersection17", "lms");

            builder.HasIndex(e => e.UnderSection6Id)
                .HasName("fkUndersection6id_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentName)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NotificationDate).HasColumnType("date");

            builder.Property(e => e.Number)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.UnderSection6Id).HasColumnType("int(11)");

            builder.HasOne(d => d.UnderSection6)
                .WithMany(p => p.Undersection17)
                .HasForeignKey(d => d.UnderSection6Id)
                .HasConstraintName("fkUndersection6id");





        }
        }
    }
