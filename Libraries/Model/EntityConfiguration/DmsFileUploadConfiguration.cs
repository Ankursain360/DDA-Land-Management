using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class DmsFileUploadConfiguration : IEntityTypeConfiguration<Dmsfileupload>
    {

        public void Configure(EntityTypeBuilder<Dmsfileupload> builder)
        {
            builder.ToTable("dmsfileupload", "lms");

            builder.HasIndex(e => e.DepartmentId)
                .HasName("fk_DepartmentIdDMSFileUpload_idx");

            builder.HasIndex(e => e.KhasraNoId)
                .HasName("fk_KhasraIdDMSFileUpload_idx");

            builder.HasIndex(e => e.LocalityId)
                .HasName("fk_LocalityIdDMSFileUpload_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.AlloteeName)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.AlmirahNo)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.FileName)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.FileNo)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.FilePath).HasColumnType("longtext");

            builder.Property(e => e.IsFileBulkUpload)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.KhasraNoId).HasColumnType("int(11)");

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.PropertyNoAddress)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Department)
                .WithMany(p => p.Dmsfileupload)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DepartmentIdDMSFileUpload");

            builder.HasOne(d => d.KhasraNo)
                .WithMany(p => p.Dmsfileupload)
                .HasForeignKey(d => d.KhasraNoId)
                .HasConstraintName("fk_KhasraIdDMSFileUpload");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Dmsfileupload)
                .HasForeignKey(d => d.LocalityId)
                .HasConstraintName("fk_LocalityIdDMSFileUpload");
        }
    }
}
