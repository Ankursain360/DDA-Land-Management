using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
   
    class IssuereturnfileConfiguration : IEntityTypeConfiguration<Issuereturnfile>
    {
        public void Configure(EntityTypeBuilder<Issuereturnfile> builder)
        {
            builder.ToTable("issuereturnfile", "lms");

            builder.HasIndex(e => e.DataStorageDetailsId)
                .HasName("fk_datastoragedetails_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.BranchId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.DataStorageDetailsId).HasColumnType("int(11)");

            builder.Property(e => e.DepartmentId).HasColumnType("int(11)");

            builder.Property(e => e.DesignationId).HasColumnType("int(11)");

            builder.Property(e => e.IssueToEmployee)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IssuingDate).HasColumnType("date");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.HasOne(d => d.DataStorageDetails)
                .WithMany(p => p.Issuereturnfile)
                .HasForeignKey(d => d.DataStorageDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_datastoragedetails");

        }
    }
}
