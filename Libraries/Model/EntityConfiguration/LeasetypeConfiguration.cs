using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class LeasetypeConfiguration : IEntityTypeConfiguration<Leasetype>
    {


        public void Configure(EntityTypeBuilder<Leasetype> builder)
        {
            //builder.ToTable("leasetype", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);
    }
    }
}
