using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {


        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("log", "lms");

            builder.Property(e => e.Id).HasColumnType("int(10) unsigned");

            builder.Property(e => e.Application)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Callsite)
                .HasMaxLength(512)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Exception).HasColumnType("longtext");

            builder.Property(e => e.Level)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Logger)
                .HasMaxLength(250)
                .IsUnicode(false);

            builder.Property(e => e.Message)
                .HasMaxLength(512)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.TraceId)
                .HasMaxLength(84)
                .IsUnicode(false);
        
    }
    }
}
