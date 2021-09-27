
using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class DemandlettersConfiguration : IEntityTypeConfiguration<Demandletters>
    {
        public void Configure(EntityTypeBuilder<Demandletters> builder)
        {

            //builder.ToTable("demandletters", "lms");

            builder.HasIndex(e => e.LocalityId)
                .HasName("fk_localityIdDemandLetters_idx");

            builder.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DamageCharges)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.DemandNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.DemandPeriodFromDate).HasColumnType("date");

            builder.Property(e => e.DemandPeriodToDate).HasColumnType("date");

            builder.Property(e => e.DepositDue)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.FatherName)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.FileNo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.GenerateDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.InterestAmount)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Penalty).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PropertyNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ReliefAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.UndersignedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.UndersignedTime)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.UptoDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Demandletters)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fk_localityIdDemandLetters");

        }
    }
}
