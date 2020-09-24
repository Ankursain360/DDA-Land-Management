using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
    public class JaraidetailConfiguration : IEntityTypeConfiguration<Jaraidetail>
    {
        public void Configure(EntityTypeBuilder<Jaraidetail> builder)
        {
            builder.ToTable("jaraidetail", "lms");
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Ahwal)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FarmerDetails)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.Kaifiyat)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.KhatauniId).HasColumnType("int(11)");

            builder.Property(e => e.KhewatId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.OldMutationNo)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.OwnerDetails)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Revenue)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TarafId).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.YearOfJamabandi).HasColumnType("date");

        }
    }
    }
