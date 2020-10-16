using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
  
     class ProposalplotdetailsConfiguration : IEntityTypeConfiguration<Proposalplotdetails>
     {

        public void Configure(EntityTypeBuilder<Proposalplotdetails> builder)
        {
            builder.ToTable("proposalplotdetails", "lms");

            builder.HasIndex(e => e.KhasraId)
                .HasName("fkProposalKhasra_idx");

            builder.HasIndex(e => e.ProposaldetailsId)
                .HasName("fkProposalDetails_idx");

            builder.HasIndex(e => e.VillageId)
                .HasName("fkProposalVillage_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ProposaldetailsId).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

            builder.HasOne(d => d.Khasra)
                .WithMany(p => p.Proposalplotdetails)
                .HasForeignKey(d => d.KhasraId)
                .HasConstraintName("fkProposalKhasra");

            builder.HasOne(d => d.Proposaldetails)
                .WithMany(p => p.Proposalplotdetails)
                .HasForeignKey(d => d.ProposaldetailsId)
                .HasConstraintName("fkProposalDetails");

            builder.HasOne(d => d.Village)
                .WithMany(p => p.Proposalplotdetails)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("fkProposalVillage");

        }
     
     }
}