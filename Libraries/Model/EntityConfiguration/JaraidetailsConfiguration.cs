using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{

    public class JaraidetailsConfiguration : IEntityTypeConfiguration<Jaraidetails>
    {
        public void Configure(EntityTypeBuilder<Jaraidetails> builder)
        {
            builder.ToTable("jaraidetails", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("FKJaraiKhasra_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("FKJaraiVilage_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NaamMalik)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.NaamPatti)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.NoOfKhatauni).HasColumnType("int(11)");

            builder.Property(e => e.NoOfKhewat).HasColumnType("int(11)");

            builder.Property(e => e.OldMutationNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.Revenue).HasColumnType("decimal(18,3)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.YearOfjamabandi).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Jaraidetails)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKJaraiKhasra");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Jaraidetails)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKJaraiVilage");

        }
    }
}
