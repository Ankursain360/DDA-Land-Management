using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class SchemefileloadingConfiguration : IEntityTypeConfiguration<Schemefileloading>
    {

        public void Configure(EntityTypeBuilder<Schemefileloading> builder)
        {

            //builder.ToTable("schemefileloading", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy)
        .IsRequired()
        .HasMaxLength(50)
        .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
        .HasColumnType("tinyint(4)")
        .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy)
        .HasMaxLength(50)
        .IsUnicode(false);

            builder.Property(e => e.SchemeCode)
        .IsRequired()
        .HasMaxLength(100)
        .IsUnicode(false);

            builder.Property(e => e.SchemeName)
        .IsRequired()
        .HasMaxLength(500)
        .IsUnicode(false);
        }
    }
}

