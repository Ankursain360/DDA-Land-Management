using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class NewlandnotificationConfiguration : IEntityTypeConfiguration<Newlandnotification>

    {
        public void Configure(EntityTypeBuilder<Newlandnotification> builder)
        {
            builder.ToTable("newlandnotification", "lms");

            builder.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

            builder.Property(e => e.NotificationTypeId).HasColumnType("int(11)");

            builder.Property(e => e.NotificationNo)
                   .HasMaxLength(200)
                   .IsUnicode(false);

            builder.Property(e => e.Date)
                    .HasMaxLength(45)
                    .IsUnicode(false);

            builder.Property(e => e.GazetteNotificationFilePath)
                  .HasMaxLength(200)
                  .IsUnicode(false);

            builder.Property(e => e.Remarks)
          .HasMaxLength(200)
          .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate)
                    .HasMaxLength(45)
                    .IsUnicode(false);

        }
    }
}
