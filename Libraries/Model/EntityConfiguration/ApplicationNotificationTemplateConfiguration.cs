using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Model.EntityConfiguration
{
    public class ApplicationNotificationTemplateConfiguration : IEntityTypeConfiguration<ApplicationNotificationTemplate>
    {
        public void Configure(EntityTypeBuilder<ApplicationNotificationTemplate> builder)
        {
            builder.ToTable("notificationtemplate", "lms");

            builder.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("bit(1)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.UserNotificationGuid)
                    .HasMaxLength(45)
                    .IsUnicode(false);

            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

            builder.Property(e => e.Template)
                    .IsRequired()
                    .HasColumnType("longtext");

            builder.Property(e => e.URL)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

        }
    }
}
