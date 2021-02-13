using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class DemandListDetailsConfiguration : IEntityTypeConfiguration<Demandlistdetails>
    {
        public void Configure(EntityTypeBuilder<Demandlistdetails> builder)
        {
            builder.ToTable("demandlistdetails", "lms");

            builder.HasIndex(e => e.KhasraNoId)
                .HasName("fk_KhasraIdDemandListDetails_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk_VillageIdDemandListDetails_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ApealableAmt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.AwardNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.BalanceInterestCase)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CourtInvolves)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DdafileNo)
                .HasColumnName("DDAFileNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.DemandListNo)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.EnhancedRatePerBigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Enmsno)
                .HasColumnName("ENMSNo")
                .HasColumnType("int(11)");

            builder.Property(e => e.ExistingRatePerBigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.KhasraNoId).HasColumnType("int(11)");

            builder.Property(e => e.LacfileNo)
                .HasColumnName("LACFileNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Lacno)
                .HasColumnName("LACNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Lbno)
                .HasColumnName("LBNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LbrefDate).HasColumnName("LBRefDate");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PartyName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PayableAmt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PayableAppealable)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ReasonForNonPay).HasColumnType("longtext");

            builder.Property(e => e.Remarks).HasColumnType("longtext");

            builder.Property(e => e.Rfano)
                .HasColumnName("RFANo")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Slpno)
                .HasColumnName("SLPNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.KhasraNo)
                .WithMany(p => p.Demandlistdetails)
                .HasForeignKey(d => d.KhasraNoId)
                .HasConstraintName("fk_KhasraIdDemandListDetails");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Demandlistdetails)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VillageIdDemandListDetails");
        }
    }
}
