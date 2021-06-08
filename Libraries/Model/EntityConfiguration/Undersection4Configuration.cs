using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
  public  class Undersection4Configuration : IEntityTypeConfiguration<Undersection4>
    {

        public void Configure(EntityTypeBuilder<Undersection4> builder)
        {
            builder.ToTable("undersection4", "lms");

            builder.HasIndex(e => e.Number)
                   .HasName("Number_UNIQUE")
                   .IsUnique();

            builder.HasIndex(e => e.ProposalId)
                .HasName("fkproposalId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.BoundaryDescription)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DocumentName)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Ndate)
                .HasColumnName("NDate")
                .HasColumnType("date");

            builder.Property(e => e.Npurpose)
                .IsRequired()
                .HasColumnName("NPurpose")
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.ProposalId).HasColumnType("int(11)");

            builder.Property(e => e.TypeDetails)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Proposal)
                .WithMany(p => p.Undersection4)
                .HasForeignKey(d => d.ProposalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkproposalId");

        }


    }
}
