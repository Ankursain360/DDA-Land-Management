using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class NewdamagepaymenthistoryConfiguration : IEntityTypeConfiguration<Newdamagepaymenthistory>
    {
        public void Configure(EntityTypeBuilder<Newdamagepaymenthistory> builder)
        {
            //builder.ToTable("newdamagepaymenthistory", "lms_local20_07_2022");

            builder.HasIndex(e => e.NewDamageSelfAssessmentId)
                .HasName("fkNewDamagepayeeRegistrationtHistoryId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Amount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.NewDamageSelfAssessmentId).HasColumnType("int(11)");

            builder.Property(e => e.PaymentDate).HasColumnType("date");

            builder.Property(e => e.PaymentMode)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.RecieptDocumentPath).HasColumnType("longtext");

            builder.Property(e => e.RecieptNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.NewDamageSelfAssessment)
                .WithMany(p => p.Newdamagepaymenthistory)
                .HasForeignKey(d => d.NewDamageSelfAssessmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkNewDamagepayeeRegistrationtHistoryId");
        }
    }
}
