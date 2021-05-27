using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
    public class NewlandenhancecompensationConfiguration : IEntityTypeConfiguration<Newlandenhancecompensation>
    {

        public void Configure(EntityTypeBuilder<Newlandenhancecompensation> builder)
        {
            builder.ToTable("newlandenhancecompensation", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fknewenhanceKhasra_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fknewenhanceVillage_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CaseInvolesWhichCourt)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CourtCaseNo)
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateOfJudgement).HasColumnType("date");

            builder.Property(e => e.DdafileNo)
                .HasColumnName("DDAFileNo")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.DemandListNo)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.EnmSno)
                .HasColumnName("EnmSNo")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.LacfileNo)
                .HasColumnName("LACFileNo")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.Lacno)
                .HasColumnName("LACNo")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.Lbno)
                .HasColumnName("LBNo")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.Lbrefdate)
                .HasColumnName("LBrefdate")
                .HasColumnType("date");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PartyName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PayableAppealable)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Remarks).HasColumnType("longtext");

            builder.Property(e => e.Rfano)
                .HasColumnName("RFANo")
                .HasMaxLength(30)
                .IsUnicode(false);

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Newlandenhancecompensation)
                .HasForeignKey(d => d.KhasraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fknewenhanceKhasra");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Newlandenhancecompensation)
                .HasForeignKey(d => d.VillageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fknewenhanceVillage");
        
        }
    }
}
