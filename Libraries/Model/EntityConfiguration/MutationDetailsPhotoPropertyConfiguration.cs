using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;

namespace Libraries.Model.builderConfiguration
{
    public class MutationDetailsPhotoPropertyConfiguration : IEntityTypeConfiguration<MutationDetailsPhotoPropertyConfiguration>
    {
        public void Configure(EntityTypeBuilder<MutationDetailsPhotoPropertyConfiguration> builder)
        {
            //builder.ToTable("mutationdetailsphotoproperty", "lms");

            //builder.HasIndex(e => e.MutationDetailsId)
            //    .HasName("MutationDetailsPhotoPropId_idx");

            //builder.Property(e => e.Id).HasColumnType("int(11)");

            //builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            //builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            //builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            //builder.Property(e => e.MutationDetailsId).HasColumnType("int(11)");

            //builder.Property(e => e.PhotoPropFilePath)
            //    .IsRequired()
            //    .HasMaxLength(1000)
            //    .IsUnicode(false);

            //builder.HasOne(d => d.MutationDetails)
            //    .WithMany(p => p.Mutationdetailsphotoproperty)
            //    .HasForeignKey(d => d.MutationDetailsId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("MutationDetailsPhotoPropId");
        }
    }
}
