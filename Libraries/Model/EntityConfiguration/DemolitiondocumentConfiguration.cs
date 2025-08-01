﻿using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class DemolitiondocumentConfiguration : IEntityTypeConfiguration<Demolitiondocument>
    {
        public void Configure(EntityTypeBuilder<Demolitiondocument> builder)
        {
            //builder.ToTable("demolitiondocument", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentName)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsMandatory)
                .IsRequired()
                .HasColumnType("char(1)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

        }
    }
    }
