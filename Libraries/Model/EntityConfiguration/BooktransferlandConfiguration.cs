
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
            builder.ToTable("booktransferland", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkBookTransferLandKhasra_idx");

            builder.HasIndex(e => e.LandNotificationId)
                .HasName("fkBookTransferLandNotification_idx");

            builder.HasIndex(e => e.LocalityId)
                .HasName("fkBookTransferLandLocality_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateofPossession).HasColumnType("date");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.LandNotificationId).HasColumnType("int(11)");

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

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

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Booktransferland)
                .HasForeignKey(d => d.KhasraId)
                .HasConstraintName("fkBookTransferLandKhasra");

            builder.HasOne(d => d.LandNotification)
                .WithMany(p => p.Booktransferland)
                .HasForeignKey(d => d.LandNotificationId)
                .HasConstraintName("fkBookTransferLandNotification");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Booktransferland)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fkBookTransferLandLocality");
        }
    }
}
