using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class NewLandDemandlistdetailsConfiguration : IEntityTypeConfiguration<Newlanddemandlistdetails>
    {
        public void Configure(EntityTypeBuilder<Newlanddemandlistdetails> builder)
        {
            builder.ToTable("demandlistdetails", "lms");

            builder.HasIndex(e => e.KhasraNoId)
                     .HasName("fk_KhasraIdNewDemandListDetails_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fk_VillageIdNewDemandListDetails_idx");

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

            //builder.Property(e => e.EnmdocumentName)
            //    .HasColumnName("ENMDocumentName")
            //    .HasMaxLength(1000)
            //    .IsUnicode(false);
            builder.Property(e => e.ENMDocumentName)
               .HasMaxLength(1000)
               .IsUnicode(false);

            builder.Property(e => e.Enmsno).HasColumnName("ENMSNo");

            builder.Property(e => e.ExistingRatePerBigha).HasColumnType("decimal(18,3)");

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

            builder.HasOne(d => d.KhasraNo)
                .WithMany(p => p.Newlanddemandlistdetails)
                .HasForeignKey(d => d.KhasraNoId)
                .HasConstraintName("fk_KhasraIdNewDemandListDetails");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Newlanddemandlistdetails)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VillageIdNewDemandListDetails");
        }
    }
}

