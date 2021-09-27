using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityConfiguration
{

    class HearingdetailsphotofiledetailsConfiguration : IEntityTypeConfiguration<Hearingdetailsphotofiledetails>
    {

        public void Configure(EntityTypeBuilder<Hearingdetailsphotofiledetails> builder)
        {

            //builder.ToTable("hearingdetailsphotofiledetails", "lms");

            builder.HasIndex(e => e.HearingDetailsId)
                .HasName("FkHearingDetailsId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Lattitude).HasColumnType("longtext");

            builder.Property(e => e.LattLongUrl).HasColumnType("longtext");

            builder.Property(e => e.Longitude).HasColumnType("longtext");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PhotoFilePath)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.HearingDetailsId).HasColumnType("int(11)");

            builder.HasOne(d => d.Hearingdetails)
                .WithMany(p => p.Hearingdetailsphotofiledetails)
                .HasForeignKey(d => d.HearingDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkHearingDetailsId");

        }
    }
}
