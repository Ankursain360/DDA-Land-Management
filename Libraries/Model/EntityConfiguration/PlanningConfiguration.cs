using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class PlanningConfiguration : IEntityTypeConfiguration<Planning>
    {
        public void Configure(EntityTypeBuilder<Planning> entity)
        {
            entity.ToTable("planning");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasColumnType("date");

            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.DivisionId).HasColumnType("int(11)");

            entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            entity.Property(e => e.IsVerify).HasColumnType("tinyint(4)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedDate).HasColumnType("date");

            entity.Property(e => e.Remarks)
                .IsRequired()
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.ZoneId).HasColumnType("int(11)");

            entity.HasOne(d => d.Department)
                .WithMany(p => p.Planning)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("PlanningDepartmentd");

            entity.HasOne(d => d.Division)
                .WithMany(p => p.Planning)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("PlanningDivisionId");

            entity.HasOne(d => d.Zone)
                .WithMany(p => p.Planning)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("PlanningZone");
        }
    }
}
