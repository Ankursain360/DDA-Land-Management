using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
   public class OnlinecomplaintConfiguration : IEntityTypeConfiguration<Onlinecomplaint>
    {
        public void Configure(EntityTypeBuilder<Onlinecomplaint> builder)
        {
            builder.ToTable("onlinecomplaint", "lms");
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AddressOfComplaint)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ComplaintTypeId).HasColumnType("int(11)");

            builder.Property(e => e.Contact)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.Lattitude)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LocationId).HasColumnType("int(11)");

            builder.Property(e => e.Longitude)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PhotoPath)
                .HasMaxLength(500)
                .IsUnicode(false);
      
        }
        }
    }
