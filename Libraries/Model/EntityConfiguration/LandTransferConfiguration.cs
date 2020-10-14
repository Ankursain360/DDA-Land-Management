using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class LandtransferConfiguration : IEntityTypeConfiguration<Landtransfer>
    {
        public void Configure(EntityTypeBuilder<Landtransfer> builder)
        {
            builder.ToTable("landtransfer");
            builder.HasIndex(e => e.DepartmentId)
                .HasName("DepartmentId_idx");

            builder.HasIndex(e => e.DivisionId)
                .HasName("LandTransferDivisionId_idx");

            builder.HasIndex(e => e.HandedOverDepartmentId)
                .HasName("LandTransferHandedOverDepartmentId_idx");

            builder.HasIndex(e => e.LocalityId)
                .HasName("LandTransferLocalityId_idx");

            builder.HasIndex(e => e.TakenOverDepartmentId)
                .HasName("LandTransferTakenOverDepartmentId_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("LandTransferZoneId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.CopyofOrderDocPath)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.DateofTakenOver).HasColumnType("date");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.DivisionId).HasColumnType("int(11)");

            builder.Property(e => e.HandedOverByNameDesingnation)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.HandedOverDate).HasColumnType("date");

            builder.Property(e => e.HandedOverDepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'1'");

            builder.Property(e => e.KhasraNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.BuildupArea).HasColumnType("decimal(10,2)");
            builder.Property(e => e.VacantArea).HasColumnType("decimal(10,2)");
            builder.Property(e => e.TotalArea).HasColumnType("decimal(10,2)");

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.OrderNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            builder.Property(e => e.TakenOverByNameDesingnation)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.TakenOverDepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.TransferorderIssueAuthority)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.LandtransferDepartment)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferDepartmentId");

            builder.HasOne(d => d.Division)
                .WithMany(p => p.Landtransfer)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferDivisionId");

            builder.HasOne(d => d.HandedOverDepartment)
                .WithMany(p => p.LandtransferHandedOverDepartment)
                .HasForeignKey(d => d.HandedOverDepartmentId)
                .HasConstraintName("LandTransferHandedOverDepartmentId");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Landtransfer)
                .HasForeignKey(d => d.LocalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferLocalityId");

            builder.HasOne(d => d.TakenOverDepartment)
                .WithMany(p => p.LandtransferTakenOverDepartment)
                .HasForeignKey(d => d.TakenOverDepartmentId)
                .HasConstraintName("LandTransferTakenOverDepartmentId");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.LandTransfer)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferZoneId");

        }
    }
}