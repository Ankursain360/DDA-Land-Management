using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class AppealdetailConfiguration : IEntityTypeConfiguration<Appealdetail>
    {

        public void Configure(EntityTypeBuilder<Appealdetail> builder)
        {
            builder.ToTable("appealdetail", "lms");

            builder.HasIndex(e => e.DemandListId)
                      .HasName("fk_DemandListIdAppealDetails_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AppealByDept)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.AppealNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateOfAppeal).HasColumnType("date");

            builder.Property(e => e.DemandListId).HasColumnType("int(11)");

            builder.Property(e => e.DemandListNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Department)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.EnmSno)
                .HasColumnName("EnmSNo")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PanelLawer)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.HasOne(d => d.DemandList)
                .WithMany(p => p.Appealdetail)
                .HasForeignKey(d => d.DemandListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_DemandListIdAppealDetails");
        }
}
}