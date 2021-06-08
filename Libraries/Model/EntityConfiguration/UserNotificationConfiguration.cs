using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class UserNotificationConfiguration : IEntityTypeConfiguration<Usernotification>
    {

        public void Configure(EntityTypeBuilder<Usernotification> builder)
        {
            builder.ToTable("usernotification", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.IsSeen)
                .HasColumnType("char(1)")
                .HasDefaultValueSql("F");

            builder.Property(e => e.Message).HasColumnType("longtext");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ProcessGuid)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.SendFrom)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.SendTo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ServiceId).HasColumnType("int(11)");

            builder.Property(e => e.UserNotificationGuid)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);
        }
    }
}
