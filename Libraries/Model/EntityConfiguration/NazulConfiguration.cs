using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class NazulConfiguration : IEntityTypeConfiguration<Nazul>
    {

        public void Configure(EntityTypeBuilder<Nazul> builder)
        {
            builder.ToTable("nazul", "lms");

            builder.HasIndex(e => e.VillageId)
                   .HasName("VillageId");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.JaraiSakni).HasColumnType("int(11)");

            builder.Property(e => e.Language).HasColumnType("int(11)");

            builder.Property(e => e.LastMutationNo)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.YearOfConsolidation).HasColumnType("date");

            builder.Property(e => e.YearOfJamabandi).HasColumnType("date");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Nazul)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("VillageId");
        }
    }
}

