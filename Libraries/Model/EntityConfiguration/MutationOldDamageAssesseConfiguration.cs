using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.builderConfiguration
{
    public class MutationOldDamageAssesseConfiguration : IEntityTypeConfiguration<Mutationolddamageassesse>
    {
        public void Configure(EntityTypeBuilder<Mutationolddamageassesse> builder)
        {
            //builder.ToTable("mutationolddamageassesse", "lms");

            builder.HasIndex(e => e.MutationDetailsId)
                .HasName("MutationDetailsId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.DateGpadead).HasColumnName("DateGPADead");

            builder.Property(e => e.FatherName)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.GpastafilePath)
                .HasColumnName("GPASTAFilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MutationDetailsId).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.HasOne(d => d.MutationDetails)
                .WithMany(p => p.Mutationolddamageassesse)
                .HasForeignKey(d => d.MutationDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_reppMutaionDetailsId");
        }
    }
}
