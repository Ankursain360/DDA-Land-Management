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

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.UnderSectionDate).HasColumnType("date");

            builder.Property(e => e.UnderSectionNotificationNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
        }


        }
    }

