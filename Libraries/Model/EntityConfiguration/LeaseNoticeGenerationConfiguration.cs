using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class LeaseNoticeGenerationConfiguration : IEntityTypeConfiguration<Leasenoticegeneration>
    {
        public void Configure(EntityTypeBuilder<Leasenoticegeneration> builder)
        {
            builder.ToTable("leasenoticegeneration", "lms");

            builder.HasIndex(e => e.RequestProceedingId)
                .HasName("fk_RequestProceedingIdNotice_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.MeetingDate).HasColumnType("date");

            builder.Property(e => e.MeetingTime)
               .HasMaxLength(20)
               .IsUnicode(false);

            builder.Property(e => e.NoticeReferenceNo)
               .HasMaxLength(200)
               .IsUnicode(false);

            builder.Property(e => e.MeetingPlace).HasColumnType("longtext");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NoticeFileName)
                .HasColumnName("NoticeFIleName")
                .HasColumnType("longtext");

            builder.Property(e => e.RequestProceedingId).HasColumnType("int(11)");

            builder.HasOne(d => d.RequestProceeding)
                .WithMany(p => p.Leasenoticegeneration)
                .HasForeignKey(d => d.RequestProceedingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RequestProceedingIdNotice");
        }
    }
}
