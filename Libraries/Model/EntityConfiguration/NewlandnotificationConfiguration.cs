using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class NewlandnotificationConfiguration : IEntityTypeConfiguration<Newlandnotification>

    {
        public void Configure(EntityTypeBuilder<Newlandnotification> builder)
        {
            //builder.ToTable("newlandnotification", "lms");

            builder.HasIndex(e => e.NotificationTypeId)
                .HasName("fk_NotificationType_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.GazetteNotificationFilePath).HasColumnType("longtext");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.NotificationNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.NotificationTypeId).HasColumnType("int(11)");

            builder.Property(e => e.Remarks).HasColumnType("longtext");

            builder.HasOne(d => d.NotificationType)
                .WithMany(p => p.Newlandnotification)
                .HasForeignKey(d => d.NotificationTypeId)
                .HasConstraintName("fk_NotificationType");
        }
    }
}
