using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", "lms");


            builder.HasIndex(e => e.DisplayName)
                .HasName("DisplayName_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.LoginName)
                .HasName("LoginName_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AadharcardNo).HasColumnType("int(11)");

            builder.Property(e => e.ChangePassword)
                .IsRequired()
                .HasMaxLength(1)
                .IsFixedLength()
                .HasDefaultValueSql("'T'");

            builder.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("datetime(6)");

            builder.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DistrictId).HasColumnType("int(11)");

            builder.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LastActivity)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LastLoginDateTime).HasColumnType("datetime(6)");

            builder.Property(e => e.LastLogoutDateTime).HasColumnType("datetime(6)");

            builder.Property(e => e.Locked)
                .IsRequired()
                .HasMaxLength(1)
                .IsFixedLength()
                .HasDefaultValueSql("'F'");

            builder.Property(e => e.LockedCount).HasColumnType("int(11)");

            builder.Property(e => e.LoginFailCount)
                .HasColumnType("int(11)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.LoginName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.LoginStatus)
                .IsRequired()
                .HasMaxLength(1)
                .IsFixedLength()
                .HasDefaultValueSql("'F'");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("datetime(6)");

            builder.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PasswordSetDate).HasColumnType("datetime(6)");

            builder.Property(e => e.PrevPassword1)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("'-'");

            builder.Property(e => e.PrevPassword2)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("'-'");

            builder.Property(e => e.PrevPassword3)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("'-'");

            builder.Property(e => e.PrevPassword4)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("'-'");

            builder.Property(e => e.PrevPassword5)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("'-'");

            builder.Property(e => e.RoleId).HasColumnType("int(11)");

            builder.Property(e => e.UserHitCount).HasColumnType("int(11)");



        }
    }
}
