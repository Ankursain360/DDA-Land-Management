using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AcquiredLandInformationManagement.DataAccess.DataObjects
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

        public virtual DbSet<Awardplotdetails> Awardplotdetails { get; set; }

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
            modelBuilder.Entity<Awardplotdetails>(entity =>
            {
                entity.ToTable("awardplotdetails");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.AwardMasterId).HasColumnType("int(11)");

                entity.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

                entity.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

                entity.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

                entity.Property(e => e.KhasraId).HasColumnType("int(11)");

                entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VillageId).HasColumnType("int(11)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
