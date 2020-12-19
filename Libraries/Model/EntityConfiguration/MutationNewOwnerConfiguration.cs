using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.builderConfiguration
{
    public class MutationNewOwnerConfiguration : IEntityTypeConfiguration<Mutationnewowner>
    {
        public void Configure(EntityTypeBuilder<Mutationnewowner> builder)
        {
            builder.ToTable("mutationnewowner", "lms");

            builder.HasIndex(e => e.MutationId)
                .HasName("MutationId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MutationId).HasColumnType("int(11)");

            builder.Property(e => e.NewOwnerAadhar)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NewOwnerAddress)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NewOwnerEmail)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NewOwnerFather)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NewOwnerGender)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NewOwnerMobile)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.NewOwnerName)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.NewOwnerPan)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PresentOwnerPhoto)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PresentOwnerSign)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Mutation)
                .WithMany(p => p.Mutationnewowner)
                .HasForeignKey(d => d.MutationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MutationNewOwnerId");
        }
    }
}
