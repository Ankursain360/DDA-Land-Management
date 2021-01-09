using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class MonthlyRosterConfiguration : IEntityTypeConfiguration<MonthlyRoaster>
    {
        public void Configure(EntityTypeBuilder<MonthlyRoaster> entity)
        {
            entity.ToTable("monthlyroaster");

            entity.HasIndex(e => e.DepartmentId)
                .HasName("MonthlyRousterDepartmentId_idx");

            entity.HasIndex(e => e.DivisionId)
                .HasName("MonthlyRousterDivisionId_idx");

            entity.HasIndex(e => e.Locality)
                .HasName("MonthlyRousterLocalityId_idx");

            entity.HasIndex(e => e.SecurityGuard)
                .HasName("MonthlyRousterSecurityGuardId_idx");

            entity.HasIndex(e => e.ZoneId)
                .HasName("MonthlyRousterZoneId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.DivisionId).HasColumnType("int(11)");

            entity.Property(e => e.Locality).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.Month).HasColumnType("int(11)");

            entity.Property(e => e.SecurityGuard).HasColumnType("int(11)");

            entity.Property(e => e.Year).HasColumnType("int(11)");

            entity.Property(e => e.ZoneId).HasColumnType("int(11)");
            entity.Property(e => e.Template).HasColumnType("Longtext");
        }
    }
}
