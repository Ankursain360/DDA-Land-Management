using Microsoft.EntityFrameworkCore;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Libraries.Model.EntityConfiguration
{
    class ProposaldetailsConfiguration : IEntityTypeConfiguration<Proposaldetails>
    {

        public void Configure(EntityTypeBuilder<Proposaldetails> builder)
        {
            builder.ToTable("proposaldetails", "lms");

            builder.HasIndex(e => e.Name)
                .HasName("Name_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.SchemeId)
                .HasName("fkProposalSchemeId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Description)
                .HasMaxLength(400)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.ProposalDate).HasColumnType("date");

            builder.Property(e => e.ProposalFileNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.RequiredAgency)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.SchemeId).HasColumnType("int(11)");

            builder.HasOne(d => d.Scheme)
                .WithMany(p => p.Proposaldetails)
                .HasForeignKey(d => d.SchemeId)
                .HasConstraintName("fkProposalSchemeId");
        }
    }
}