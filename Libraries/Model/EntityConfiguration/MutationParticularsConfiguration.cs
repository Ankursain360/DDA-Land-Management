using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class MutationParticularsConfiguration : IEntityTypeConfiguration<Mutationparticulars>
    {
        public void Configure(EntityTypeBuilder<Mutationparticulars> builder)
        {
            builder.ToTable("mutationparticulars", "lms");

            builder.HasIndex(e => e.MutationId)
                .HasName("fk_MutaionIdMutationparticlar_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.FatherName)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.MutationId).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.Share)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.HasOne(d => d.Mutation)
                .WithMany(p => p.Mutationparticulars)
                .HasForeignKey(d => d.MutationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MutaionIdMutationparticlar");
        }
    }
}
