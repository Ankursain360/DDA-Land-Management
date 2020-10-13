using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{
    public class EncroachmentLocationMapFileDetailsConfiguration : IEntityTypeConfiguration<EncroachmentLocationMapFileDetails>
    {
        public void Configure(EntityTypeBuilder<EncroachmentLocationMapFileDetails> builder)
        {
            builder.ToTable("encroachmentlocationmapfiledetails");

            builder.HasIndex(e => e.EncroachmentRegistrationId)
                .HasName("EncroachmentRegistrationLocationMapId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.EncroachmentRegistrationId).HasColumnType("int(11)");

            builder.Property(e => e.LocationMapFilePath)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.HasOne(d => d.EncroachmentRegistration)
                .WithMany(p => p.EncroachmentLocationMapFileDetails)
                .HasForeignKey(d => d.EncroachmentRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EncroachmentRegistrationLocationMapId");
        }
    }
}
