using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.builderConfiguration
{
    public class MutationOriginalOwnerConfiguration : IEntityTypeConfiguration<Mutationoriginalowner>
    {
        public void Configure(EntityTypeBuilder<Mutationoriginalowner> builder)
        {
            builder.ToTable("mutationoriginalowner", "lms");

            builder.HasIndex(e => e.MutationId)
                .HasName("MutationId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MutationId).HasColumnType("int(11)");

            builder.Property(e => e.OriginalOwnerAadhar)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.OriginalOwnerAddress)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.OriginalOwnerFather)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.OriginalOwnerGender)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.OriginalOwnerMobile)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.OriginalOwnerName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.OriginalOwnerPan)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PresentOwnerAadharNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PresentOwnerPanNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PresentOwnerPhoto)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PresentOwnerSign)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Status)
                .HasMaxLength(2)
                .IsUnicode(false);

            builder.HasOne(d => d.Mutation)
                .WithMany(p => p.Mutationoriginalowner)
                .HasForeignKey(d => d.MutationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MutationOriginalId");
        }
    }
}
