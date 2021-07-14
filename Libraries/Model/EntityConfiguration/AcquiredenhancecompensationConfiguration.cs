//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Model.EntityConfiguration
//{
//    class AcquiredenhancecompensationConfiguration
//    {
//    }
//}
using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AcquiredenhancecompensationConfiguration : IEntityTypeConfiguration<Acquiredenhancecompensation>
    {


        public void Configure(EntityTypeBuilder<Acquiredenhancecompensation> entity)
        {
            entity.ToTable("acquiredenhancecompensation", "lms");

            entity.HasIndex(e => e.KhasraNoId)
                .HasName("fk_KhasraIdAcqDemandListDetails_idx");

            entity.HasIndex(e => e.VillageId)
                .HasName("fk_VillageIdAcqDemandListDetails_idx");

            entity.Property(e => e.AmountPaid).HasColumnType("decimal(18,3)");

            entity.Property(e => e.ApDemandListNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.ApEnmSNo)
                .HasColumnName("ApEnmSNo")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.ApealableAmt).HasColumnType("decimal(18,3)");

            entity.Property(e => e.AppealByDept)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.AppealNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.AwardNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.BalanceInterestCase)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.BankName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ChequeDate).HasColumnType("date");

            entity.Property(e => e.ChequeNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.CourtInvolves)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.Property(e => e.DateOfAppeal).HasColumnType("date");

            entity.Property(e => e.DDAFileNo)
                .HasColumnName("DDAFileNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.DemandListNo)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.Department)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.EnhancedRatePerBigha).HasColumnType("decimal(18,3)");

            entity.Property(e => e.ENMDocumentName)
                .HasColumnName("ENMDocumentName")
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.ENMSNo).HasColumnName("ENMSNo");

            entity.Property(e => e.ExistingRatePerBigha).HasColumnType("decimal(18,3)");

            entity.Property(e => e.LACFileNo)
                .HasColumnName("LACFileNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.LACNo)
                .HasColumnName("LACNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.LBNo)
                .HasColumnName("LBNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.LBRefDate).HasColumnName("LBRefDate");

            entity.Property(e => e.PanelLawer)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.PartyName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.PayDemandListNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.PayEnmSNo)
                .HasColumnName("PayEnmSNo")
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.PayableAmt).HasColumnType("decimal(18,3)");

            entity.Property(e => e.PayableAppealable)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.PaymentProofDocumentName)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.Property(e => e.PercentPaid).HasColumnType("decimal(18,3)");

            entity.Property(e => e.ReasonForNonPay).HasColumnType("longtext");

            entity.Property(e => e.Remarks).HasColumnType("longtext");

            entity.Property(e => e.RFANo)
                .HasColumnName("RFANo")
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.SLPNo)
                .HasColumnName("SLPNo")
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,3)");

            entity.Property(e => e.VoucherNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.KhasraNo)
                .WithMany(p => p.Acquiredenhancecompensation)
                .HasForeignKey(d => d.KhasraNoId)
                .HasConstraintName("fk_KhasraIdAcqDemandListDetails");

            entity.HasOne(d => d.Village)
                .WithMany(p => p.Acquiredenhancecompensation)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VillageIdAcqDemandListDetails");
        

        
        }

}
}
