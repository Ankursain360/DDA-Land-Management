using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SiteMaster.DataAccess.DataObjects
{
    public partial class lmsContext : DbContext
    {
        public lmsContext()
        {
        }

        public lmsContext(DbContextOptions<lmsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=49.50.87.108;port=3306;user=root;password=Google@123;database=lms");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.DisplayName)
                    .HasName("DisplayName_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.LoginName)
                    .HasName("LoginName_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AadharcardNo).HasColumnType("int(11)");

                entity.Property(e => e.ChangePassword)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasDefaultValueSql("'T'");

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime(6)");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictId).HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

                entity.Property(e => e.LastActivity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginDateTime).HasColumnType("datetime(6)");

                entity.Property(e => e.LastLogoutDateTime).HasColumnType("datetime(6)");

                entity.Property(e => e.Locked)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasDefaultValueSql("'F'");

                entity.Property(e => e.LockedCount).HasColumnType("int(11)");

                entity.Property(e => e.LoginFailCount)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LoginStatus)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasDefaultValueSql("'F'");

                entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime(6)");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSetDate).HasColumnType("datetime(6)");

                entity.Property(e => e.PrevPassword1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'-'");

                entity.Property(e => e.PrevPassword2)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'-'");

                entity.Property(e => e.PrevPassword3)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'-'");

                entity.Property(e => e.PrevPassword4)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'-'");

                entity.Property(e => e.PrevPassword5)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("'-'");

                entity.Property(e => e.RoleId).HasColumnType("int(11)");

                entity.Property(e => e.UserHitCount).HasColumnType("int(11)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
