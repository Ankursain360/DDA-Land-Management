
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
   
    class BooktransferlandConfiguration : IEntityTypeConfiguration<Booktransferland>
    {

        public void Configure(EntityTypeBuilder<Booktransferland> builder)
        {
            //builder.ToTable("booktransferland", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkBookTransferLandKhasra_idx");

            builder.HasIndex(e => e.LocalityId)
                .HasName("fkBookTransferLandLocality_idx");

            builder.HasIndex(e => e.OtherLandNotificationId)
                .HasName("fk_OtherlandNotif_idx");

            builder.Property(e => e.Area).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateofPossession).HasColumnType("date");

            builder.Property(e => e.NotificationDate).HasColumnType("date");

            builder.Property(e => e.Part)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.StatusOfLand)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.HasOne(d => d.OtherLandNotification)
                .WithMany(p => p.Booktransferland)
                .HasForeignKey(d => d.OtherLandNotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OtherlandNotif");
        }
    }
}
