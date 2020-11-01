using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class LandtransferConfiguration : IEntityTypeConfiguration<Landtransfer>
    {
        public void Configure(EntityTypeBuilder<Landtransfer> entity)
        {
            entity.ToTable("landtransfer");

            entity.HasIndex(e => e.DepartmentId)
                .HasName("DepartmentId_idx");

            entity.HasIndex(e => e.DivisionId)
                .HasName("LandTransferDivisionId_idx");

            entity.HasIndex(e => e.HandedOverDepartmentId)
                .HasName("LandTransferHandedOverDepartmentId_idx");

            entity.HasIndex(e => e.HandedOverDivisionId)
                .HasName("LandTransferHandedOverDivisionId_idx");

            entity.HasIndex(e => e.HandedOverZoneId)
                .HasName("LandTransferHandedOverZoneId_idx");

            entity.HasIndex(e => e.LocalityId)
                .HasName("LandTransferLocalityId_idx");

            entity.HasIndex(e => e.PropertyRegistrationId)
                .HasName("LandTransferPropertyRegistrationId_idx");

            entity.HasIndex(e => e.TakenOverDepartmentId)
                .HasName("LandTransferTakenOverDepartmentId_idx");

            entity.HasIndex(e => e.TakenOverDivisionId)
                .HasName("LandTransferTakenOverDivisionId_idx");

            entity.HasIndex(e => e.TakenOverZoneId)
                .HasName("LandTransferTakenOverZoneId_idx");

            entity.HasIndex(e => e.ZoneId)
                .HasName("LandTransferZoneId_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.ActionOnEncroachment)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ActionTakenReportPath)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.BuildUpInEncroachementArea).HasColumnType("decimal(18,2)");

            entity.Property(e => e.BuildupArea).HasColumnType("decimal(10,2)");

            entity.Property(e => e.CopyofOrderDocPath)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasColumnType("date");

            entity.Property(e => e.DateofTakenOver).HasColumnType("date");

            entity.Property(e => e.DepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.DivisionId).HasColumnType("int(11)");

            entity.Property(e => e.EncroachementArea).HasColumnType("decimal(18,2)");

            entity.Property(e => e.Encroachment).HasColumnType("tinyint(4)");

            entity.Property(e => e.EncroachmentDetails)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.EncroachmentStatus).HasColumnType("int(11)");

            entity.Property(e => e.HandedOverByNameDesingnation)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.HandedOverCommments)
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.HandedOverDate).HasColumnType("date");

            entity.Property(e => e.HandedOverDepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.HandedOverDivisionId).HasColumnType("int(11)");

            entity.Property(e => e.HandedOverEmailId)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.HandedOverLandLineNo).HasColumnType("decimal(12,0)");

            entity.Property(e => e.HandedOverMobileNo).HasColumnType("decimal(12,0)");

            entity.Property(e => e.HandedOverZoneId).HasColumnType("int(11)");

            entity.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'1'");

            entity.Property(e => e.KhasraNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.LocalityId).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedDate).HasColumnType("date");

            entity.Property(e => e.OrderNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.PropertyRegistrationId).HasColumnType("int(11)");

            entity.Property(e => e.Remarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.TakenOverByNameDesingnation)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TakenOverCommments)
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.TakenOverDepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.TakenOverDivisionId).HasColumnType("int(11)");

            entity.Property(e => e.TakenOverDocument)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.TakenOverEmailId)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.TakenOverLandLineNo).HasColumnType("decimal(12,0)");

            entity.Property(e => e.TakenOverMobileNo).HasColumnType("decimal(12,0)");

            entity.Property(e => e.TakenOverZoneId).HasColumnType("int(11)");

            entity.Property(e => e.TotalArea).HasColumnType("decimal(10,2)");

            entity.Property(e => e.TransferorderIssueAuthority)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.VacantArea).HasColumnType("decimal(10,2)");

            entity.Property(e => e.ZoneId).HasColumnType("int(11)");

            entity.HasOne(d => d.Department)
                .WithMany(p => p.LandtransferDepartment)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferDepartmentId");

            entity.HasOne(d => d.Division)
                .WithMany(p => p.LandtransferDivision)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferDivisionId");

            entity.HasOne(d => d.HandedOverDepartment)
                .WithMany(p => p.LandtransferHandedOverDepartment)
                .HasForeignKey(d => d.HandedOverDepartmentId)
                .HasConstraintName("LandTransferHandedOverDepartmentId");

            entity.HasOne(d => d.HandedOverDivision)
                .WithMany(p => p.LandtransferHandedOverDivision)
                .HasForeignKey(d => d.HandedOverDivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferHandedOverDivisionId");

            entity.HasOne(d => d.HandedOverZone)
                .WithMany(p => p.LandtransferHandedOverZone)
                .HasForeignKey(d => d.HandedOverZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferHandedOverZoneId");

            entity.HasOne(d => d.Locality)
                .WithMany(p => p.Landtransfer)
                .HasForeignKey(d => d.LocalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferLocalityId");

            entity.HasOne(d => d.PropertyRegistration)
                .WithMany(p => p.Landtransfer)
                .HasForeignKey(d => d.PropertyRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferPropertyRegistrationId");

            entity.HasOne(d => d.TakenOverDepartment)
                .WithMany(p => p.LandtransferTakenOverDepartment)
                .HasForeignKey(d => d.TakenOverDepartmentId)
                .HasConstraintName("LandTransferTakenOverDepartmentId");

            entity.HasOne(d => d.TakenOverDivision)
                .WithMany(p => p.LandtransferTakenOverDivision)
                .HasForeignKey(d => d.TakenOverDivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferTakenOverDivisionId");

            entity.HasOne(d => d.TakenOverZone)
                .WithMany(p => p.LandtransferTakenOverZone)
                .HasForeignKey(d => d.TakenOverZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferTakenOverZoneId");

            entity.HasOne(d => d.Zone)
                .WithMany(p => p.LandtransferZone)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LandTransferZoneId");
        }
    }
}