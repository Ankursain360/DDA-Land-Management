using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Libraries.Model.EntityConfiguration
{
  public  class EnhancecompensationConfiguration: IEntityTypeConfiguration<Enhancecompensation>
    {
        public void Configure(EntityTypeBuilder<Enhancecompensation> builder)
        {
            builder.ToTable("enhancecompensation", "lms");
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AmountPaid)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.AppealDept)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.AppealNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.BankName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CaseCourt)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ChequeDate).HasColumnType("date");

            builder.Property(e => e.ChequeNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateOfAppeal).HasColumnType("date");

            builder.Property(e => e.DdafileNo)
                .HasColumnName("DDAFileNo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DemandListNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DemandStatus).HasColumnType("tinyint(4)");

            builder.Property(e => e.Enmsno)
                .HasColumnName("ENMSNo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.JudgementDate).HasColumnType("date");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.LacfileNo)
                .HasColumnName("LACFileNo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Lacno)
                .HasColumnName("LACNo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Lbno)
                .HasColumnName("LBNo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LbrefDate)
                .HasColumnName("LBrefDate")
                .HasColumnType("date");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PanelLawer)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PartyName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Payable)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PaymentStatus).HasColumnType("tinyint(4)");

            builder.Property(e => e.PercentPaid)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(150)
                .IsUnicode(false);

            builder.Property(e => e.Rfano)
                .HasColumnName("RFANo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Slpno)
                .HasColumnName("SLPNo")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.Property(e => e.VoucherNo)
                .HasMaxLength(100)
                .IsUnicode(false);
      

        }
}
    }
