using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
     class DamagepayeeregisterConfiguration : IEntityTypeConfiguration<Damagepayeeregister>
    {
        public void Configure(EntityTypeBuilder<Damagepayeeregister> builder)
        {
            builder.ToTable("damagepayeeregister", "lms");

            builder.HasIndex(e => e.DistrictId)
                .HasName("Fk_District_idx");

            builder.HasIndex(e => e.Id)
                .HasName("Id_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.LocalityId)
                .HasName("fk_Locality_idx");

            builder.HasIndex(e => e.OnlinePaymentLocalityId)
                .HasName("fk_OnlinepaymntLocality_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

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

            builder.Property(e => e.DamageProperty).HasColumnType("int(11)");

            builder.Property(e => e.DistrictId).HasColumnType("int(11)");

            builder.Property(e => e.EncroachmentDate).HasColumnType("date");

            builder.Property(e => e.EndDate).HasColumnType("date");

            builder.Property(e => e.FileNo).HasColumnType("int(11)");

            builder.Property(e => e.FloorAreaSqMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.FloorAreaSqYard).HasColumnType("decimal(18,3)");

            builder.Property(e => e.FloorNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsDdadamagePayee)
                .HasColumnName("IsDDADamagePayee")
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LitigationStatus)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.OnlinePaymentAreaSqYds).HasColumnType("decimal(18,3)");

            builder.Property(e => e.OnlinePaymentLocalityId).HasColumnType("int(11)");

            builder.Property(e => e.OppositionName)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PetitionerRespondent)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PinCode)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PlotAreaSqMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PlotAreaSqYard).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PropertyNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ResidentialSqMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ResidentialSqYard).HasColumnType("decimal(18,3)");

            builder.Property(e => e.StartDate).HasColumnType("date");

            builder.Property(e => e.StreetNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.TypeOfDamageAssessee)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.UseOfProperty)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.District)
                .WithMany(p => p.Damagepayeeregister)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("Fk_District");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.DamagepayeeregisterLocality)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fk_Locality");

            builder.HasOne(d => d.OnlinePaymentLocality)
                .WithMany(p => p.DamagepayeeregisterOnlinePaymentLocality)
                .HasForeignKey(d => d.OnlinePaymentLocalityId)
                .HasConstraintName("fk_OnlinepaymntLocality");

        }
    }
}
