﻿using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
        class DisposallandtypeConfiguration : IEntityTypeConfiguration<Disposallandtype>
    {

        public void Configure(EntityTypeBuilder<Disposallandtype> builder)
        {

            //builder.ToTable("disposallandtype", "lms");

            builder.HasIndex(e => e.Name)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.LandCode)
                   .HasMaxLength(10)
                   .IsUnicode(false);

            builder.Property(e => e.RecState).HasColumnType("char(1)");

        }
    }
}
