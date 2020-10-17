using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{

    class NazullandConfiguration : IEntityTypeConfiguration<Nazulland>
    {
        public void Configure(EntityTypeBuilder<Nazulland> builder)
        {
            builder.ToTable("nazulland", "lms");

            builder.HasIndex(e => e.DivisionId)
                .HasName("FKnazullandDivision_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AdiCourt)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.AmountOfAward).HasColumnType("int(11)");

            builder.Property(e => e.AreaOfWhichPossessionTakenOver)
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.AwardDate).HasColumnType("date");

            builder.Property(e => e.AwardNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CertificateToCorrectnessOfEntry)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateOfPossession).HasColumnType("date");

            builder.Property(e => e.DateOfTransfer).HasColumnType("date");

            builder.Property(e => e.DateOnWhichPossessionTakenOver).HasColumnType("date");

            builder.Property(e => e.DivisionId).HasColumnType("int(11)");

            builder.Property(e => e.HighCourt)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.LandAreaAcquired)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.SchemeForWhichAcquired)
                .HasMaxLength(300)
                .IsUnicode(false);

            builder.Property(e => e.SupremeCourt)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.Division)
                .WithMany(p => p.Nazulland)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("fkdivisionnazulland");

            

        }
    }
}