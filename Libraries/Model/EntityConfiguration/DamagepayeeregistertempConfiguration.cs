using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class DamagepayeeregistertempConfiguration : IEntityTypeConfiguration<Damagepayeeregistertemp>
    {
        public void Configure(EntityTypeBuilder<Damagepayeeregistertemp> builder)
        {

            builder.ToTable("damagepayeeregistertemp", "lms");

            builder.HasIndex(e => e.DistrictId)
                .HasName("Fk_TempDistrict_idx");

            builder.HasIndex(e => e.Id)
                .HasName("Id_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.LocalityId)
                .HasName("fk_TempLocality_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Achknowledgement).HasColumnType("longtext");

            builder.Property(e => e.ApprovedStatus).HasColumnType("int(11)");

            builder.Property(e => e.CalculatorValue).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CaseNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CommercialSqMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CommercialSqYard).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CourtName)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Declaration1).HasColumnType("int(11)");

            builder.Property(e => e.Declaration2).HasColumnType("int(11)");

            builder.Property(e => e.Declaration3).HasColumnType("int(11)");

            builder.Property(e => e.DistrictId).HasColumnType("int(11)");

            builder.Property(e => e.DocumentForFilePath).HasColumnType("longtext");

            builder.Property(e => e.FgformPath)
                .HasColumnName("FGFormPath")
                .HasColumnType("longtext");

            builder.Property(e => e.FileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.FloorAreaSqMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.FloorAreaSqYard).HasColumnType("decimal(18,3)");

            builder.Property(e => e.FloorNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.InterestDueAmountCompund).HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsApplyForMutation).HasColumnType("int(11)");

            builder.Property(e => e.IsDdadamagePayee)
                .HasColumnName("IsDDADamagePayee")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IsDocumentFor)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LitigationStatus)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.OppositionName)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Otp)
                .HasColumnName("OTP")
                .HasColumnType("int(11)");

            builder.Property(e => e.PendingAt).HasColumnType("int(11)");

            builder.Property(e => e.UserId).HasColumnType("int(11)");

            builder.Property(e => e.PetitionerRespondent)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PinCode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PlotAreaSqMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PlotAreaSqYard).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ProceedToPay).HasColumnType("int(11)");

            builder.Property(e => e.PropertyNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PropertyPhotoPath).HasColumnType("longtext");

            builder.Property(e => e.Rebate).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ResidentialSqMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ResidentialSqYard).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ShowCauseNoticePath).HasColumnType("longtext");

            builder.Property(e => e.Signature).HasColumnType("longtext");

            builder.Property(e => e.StreetNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TotalPayable).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TotalValueWithInterest).HasColumnType("decimal(18,3)");

            builder.Property(e => e.TypeOfDamageAssessee)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.UseOfProperty)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.District)
                .WithMany(p => p.Damagepayeeregistertemp)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("Fk_TempDistrict");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Damagepayeeregistertemp)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fk_TempLocality");


        }
    }
}
