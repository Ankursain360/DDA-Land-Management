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

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.CopyofOrderDocPath)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

            entity.Property(e => e.CreatedDate).HasColumnType("date");

            entity.Property(e => e.DateofTakenOver).HasColumnType("date");

            entity.Property(e => e.DivisionId).HasColumnType("int(11)");

            entity.Property(e => e.FileName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.HandedOverByNameDesingnation)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.HandedOverDate).HasColumnType("date");

            entity.Property(e => e.HandedOverDepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("'1'");

            entity.Property(e => e.KhasraNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            entity.Property(e => e.ModifiedDate).HasColumnType("date");

            entity.Property(e => e.OrderNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Remarks)
                .HasMaxLength(5000)
                .IsUnicode(false);

            entity.Property(e => e.TakenOverByNameDesingnation)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TakenOverDepartmentId).HasColumnType("int(11)");

            entity.Property(e => e.TransferorderIssueAuthority)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.VillageId).HasColumnType("int(11)");

            entity.Property(e => e.ZoneId).HasColumnType("int(11)");
        }
    }
}