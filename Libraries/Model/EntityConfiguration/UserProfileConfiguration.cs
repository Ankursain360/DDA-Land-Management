using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Entity;

namespace Model.EntityConfiguration
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<Userprofile>
    {
        public void Configure(EntityTypeBuilder<Userprofile> builder)
        {
            builder.ToTable("userprofile", "lms");

            builder.HasIndex(e => e.BranchId)
                    .HasName("fk_BranchIdUserprofile_idx");

            builder.HasIndex(e => e.DepartmentId)
                .HasName("FK_Department_idx_idx");

            builder.HasIndex(e => e.DistrictId)
                .HasName("FK_District_idx");

            builder.HasIndex(e => e.RoleId)
                .HasName("FK_Role_idx_idx");

            builder.HasIndex(e => e.UserId)
                .HasName("FK_User_ids_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("FK_Zone_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.BranchId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.DistrictId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("bit(1)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.RoleId).HasColumnType("int(11)");

            builder.Property(e => e.UserId).HasColumnType("int(11)");

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.Branch)
                .WithMany(p => p.Userprofile)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("fk_BranchIdUserprofile");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Userprofile)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Department_idx");

            builder.HasOne(d => d.District)
                .WithMany(p => p.Userprofile)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_District_idx");

            builder.HasOne(d => d.Role)
                .WithMany(p => p.Userprofile)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Role_idx");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Userprofile)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_ids");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Userprofile)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("FK_Zone_idx");
        }
    }
}
