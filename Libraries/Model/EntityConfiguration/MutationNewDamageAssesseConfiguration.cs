using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.builderConfiguration
{
    public class MutationNewDamageAssesseConfiguration : IEntityTypeConfiguration<Mutationnewdamageassesse>
    {
        public void Configure(EntityTypeBuilder<Mutationnewdamageassesse> builder)
        {
            builder.ToTable("mutationnewdamageassesse", "lms");

            builder.HasIndex(e => e.MutationDetailsId)
                .HasName("MutationDetailsId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AadharNo)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.Email)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Gender).HasColumnType("int(11)");

            builder.Property(e => e.GuardianName)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.MobileNo)
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MutationDetailsId).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PanNo)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.PhotoFilePath).HasColumnType("longtext");

            builder.Property(e => e.SignatureFilePath).HasColumnType("longtext");

            builder.HasOne(d => d.MutationDetails)
                .WithMany(p => p.Mutationnewdamageassesse)
                .HasForeignKey(d => d.MutationDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reppMutationDetailsId");
        }
    }
}
