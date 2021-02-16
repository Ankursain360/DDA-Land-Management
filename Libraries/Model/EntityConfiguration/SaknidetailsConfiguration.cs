using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{ 
     class SaknidetailsConfiguration : IEntityTypeConfiguration<Saknidetails>
     {
        public void Configure(EntityTypeBuilder<Saknidetails> builder)
        {
            builder.ToTable("saknidetails", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("FKsaknikhasra_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("FKsaknivillage_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.Location)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Mortgage)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.NoOfKhatauni).HasColumnType("int(11)");

            builder.Property(e => e.NoOfKhewat).HasColumnType("int(11)");

            builder.Property(e => e.OldMutationNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.RentAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.YearOfjamabandi).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Saknidetails)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKsaknikhasra");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Saknidetails)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKsaknivillage");

        }
    }
}
