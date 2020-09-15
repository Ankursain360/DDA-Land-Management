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

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.UnderSection6Id).HasColumnType("int(11)");

            builder.Property(e => e.Us17date)
                .HasColumnName("US17Date")
                .HasColumnType("date");

            builder.Property(e => e.Us17number)
                .HasColumnName("US17Number")
                .HasMaxLength(100)
                .IsUnicode(false);
      




        }
        }
    }
