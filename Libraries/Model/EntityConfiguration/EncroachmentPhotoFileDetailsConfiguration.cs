using Libraries.Model.Common;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    class EncroachmentPhotoFileDetailsConfiguration : IEntityTypeConfiguration<EncroachmentPhotoFileDetails>
    {
        public void Configure(EntityTypeBuilder<EncroachmentPhotoFileDetails> builder)
        {
            builder.ToTable("encroachmentphotofiledetails");
            builder.HasIndex(e => e.EncroachmentRegistrationId)
                .HasName("EncroachmentRegistrationPhotoId_idx");
            builder.Property(e => e.Id).HasColumnType("int(11)");
            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");
            builder.Property(e => e.EncroachmentRegistrationId).HasColumnType("int(11)");
            builder.Property(e => e.PhotoFilePath)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);
            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");
            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            builder.HasOne(d => d.EncroachmentRegistration)
                .WithMany(p => p.EncroachmentPhotoFileDetails)
                .HasForeignKey(d => d.EncroachmentRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EncroachmentRegistrationPhotoId");
        }
    }
}
