﻿using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    class LdolandConfiguration : IEntityTypeConfiguration<Ldoland>
    {
        public void Configure(EntityTypeBuilder<Ldoland> builder)
        {
            //builder.ToTable("ldoland", "lms");

            
            builder.HasIndex(e => e.OtherLandNotificationId)
                    .HasName("fk_otherlandnotifId_idx");
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Area).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateofPossession).HasColumnType("date");

            builder.Property(e => e.DateOfLandResume).HasColumnType("date");

            builder.Property(e => e.GOINotificationDocumentName)
                .HasMaxLength(1000)
                .IsUnicode(false);


            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.OtherLandNotificationId).HasColumnType("int(11)");

            builder.Property(e => e.Location)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.NotificationDate).HasColumnType("date");

            builder.Property(e => e.OccupiedBy)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.OrderDocumentName)
                .HasMaxLength(1000)
                .IsUnicode(false);


            builder.Property(e => e.PossessionDocumentName)
                .HasMaxLength(1000)
                .IsUnicode(false);


            builder.Property(e => e.PropertySiteNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(200)
                .IsUnicode(false);
            builder.Property(e => e.SerialNumber)
                .HasMaxLength(200)
                .IsUnicode(false);
            //  builder.Property(e => e.SerialnumberId).HasColumnType("int(11)");

            builder.Property(e => e.SiteDescription)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.StatusOfLand)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.OtherLandNotification)
                    .WithMany(p => p.Ldoland)
                    .HasForeignKey(d => d.OtherLandNotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_otherlandnotifId");


        }

    }

}
