using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
    public class DataStorageConfiguration : IEntityTypeConfiguration<Datastoragedetails>
    {

        public void Configure(EntityTypeBuilder<Datastoragedetails> builder)
        {

            //builder.ToTable("datastoragedetails", "lms");

            builder.HasIndex(e => e.AlmirahId)
                .HasName("fk_AlmirahId");

            builder.HasIndex(e => e.BranchId)
                .HasName("fk_BranchId_idx");

            builder.HasIndex(e => e.BundleId)
                .HasName("BundleNo_idx");

            builder.HasIndex(e => e.ColumnId)
                .HasName("ColumnNo_idx");

            builder.HasIndex(e => e.DepartmentId)
                .HasName("fk_DataDeartment_idx");

            builder.HasIndex(e => e.LocalityId)
                .HasName("LocalityId_idx");

            builder.HasIndex(e => e.RowId)
                .HasName("RowNo_idx");

            builder.HasIndex(e => e.SchemeId)
                .HasName("fk_SchemeFileLoading_idx");

            builder.HasIndex(e => e.ZoneId)
                .HasName("ZoneId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AlmirahId).HasColumnType("int(11)");

            builder.Property(e => e.Area)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.BlockId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.BranchId).HasColumnType("int(11)");

            builder.Property(e => e.BranchSno).HasColumnType("int(11)");

            builder.Property(e => e.BundleId).HasColumnType("int(11)");

            builder.Property(e => e.CategoryNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ColumnId).HasColumnType("int(11)");

            builder.Property(e => e.CompactorNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.DocumentSequenceNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.DocumentType)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.FileNo)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.FileStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.FlatCategoryId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.FlatNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.HeaderNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.IsFileDocument).HasColumnType("int(11)");

            builder.Property(e => e.IsFreeHold)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IsPartOfMainFile).HasColumnType("int(11)");

            builder.Property(e => e.KhasraNoPropertyNo)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.PocketId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.RecordRoomNo)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.RowId).HasColumnType("int(11)");

            builder.Property(e => e.SchemeId).HasColumnType("int(11)");

            builder.Property(e => e.SectorId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.SequenceNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.SttsNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.UserId).HasColumnType("int(11)");

            builder.Property(e => e.Year).HasColumnType("int(11)");

            builder.Property(e => e.YearTo).HasColumnType("int(11)");

            builder.Property(e => e.ZoneId).HasColumnType("int(11)");

            builder.HasOne(d => d.Almirah)
                .WithMany(p => p.Datastoragedetails)
                .HasForeignKey(d => d.AlmirahId)
                .HasConstraintName("fk_AlmirahId");

            builder.HasOne(d => d.Branch)
                .WithMany(p => p.Datastoragedetails)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("fk_BranchId");

            builder.HasOne(d => d.Bundle)
                .WithMany(p => p.Datastoragedetails)
                .HasForeignKey(d => d.BundleId)
                .HasConstraintName("fk_BundleId");

            builder.HasOne(d => d.Column)
                .WithMany(p => p.Datastoragedetails)
                .HasForeignKey(d => d.ColumnId)
                .HasConstraintName("fk_ColumnId");

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Datastoragedetails)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("fk_DataDeartment");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Datastoragedetails)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fk_LocalityIdForDataStorage");

            builder.HasOne(d => d.Row)
                .WithMany(p => p.Datastoragedetails)
                .HasForeignKey(d => d.RowId)
                .HasConstraintName("fk_RowId");

            builder.HasOne(d => d.SchemeFileLoading)
                .WithMany(p => p.Datastoragedetails)
                .HasForeignKey(d => d.SchemeId)
                .HasConstraintName("fk_SchemeFileLoading");

            builder.HasOne(d => d.Zone)
                .WithMany(p => p.Datastoragedetails)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("fk_ZoneIdForDataStorage");


        }
    }
}