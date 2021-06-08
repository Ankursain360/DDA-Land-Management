using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{

    class Undersection6Configuration : IEntityTypeConfiguration<Undersection6>
    {
        public void Configure(EntityTypeBuilder<Undersection6> builder)
        {

            builder.ToTable("undersection6", "lms");

            builder.HasIndex(e => e.Undersection4Id)
                .HasName("usd4id_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentName)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Ndate)
                .HasColumnName("NDate")
                .HasColumnType("date");

            builder.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Undersection4Id)
                .HasColumnName("undersection4Id")
                .HasColumnType("int(11)");

            builder.HasOne(d => d.Undersection4)
                .WithMany(p => p.Undersection6)
                .HasForeignKey(d => d.Undersection4Id)
                .HasConstraintName("fkundersection4Id");
        }


        }
    }

