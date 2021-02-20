using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{


    public class NewlandacquistionproposalplotdetailsConfiguration : IEntityTypeConfiguration<Newlandacquistionproposalplotdetails>
    {

        public void Configure(EntityTypeBuilder<Newlandacquistionproposalplotdetails> builder)
        {
            builder.ToTable("newlandacquistionproposalplotdetails", "lms");

            builder.HasIndex(e => e.AcquiredlandvillageId)
                    .HasName("fknewVillage_idx");

            builder.HasIndex(e => e.KhasraId)
                    .HasName("fknewProposalKhasra_idx");

            builder.HasIndex(e => e.ProposaldetailsId)
                    .HasName("fknewProposalDetails_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AcquiredlandvillageId).HasColumnType("int(11)");

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

            builder.HasOne(d => d.Acquiredlandvillage)
                    .WithMany(p => p.Newlandacquistionproposalplotdetails)
                    .HasForeignKey(d => d.AcquiredlandvillageId)
                    .HasConstraintName("fknewVillage");

            builder.HasOne(d => d.Khasra)
                    .WithMany(p => p.Newlandacquistionproposalplotdetails)
                    .HasForeignKey(d => d.KhasraId)
                    .HasConstraintName("fknewProposalKhasra");

            builder.HasOne(d => d.Proposaldetails)
                    .WithMany(p => p.Newlandacquistionproposalplotdetails)
                    .HasForeignKey(d => d.ProposaldetailsId)
                    .HasConstraintName("fknewProposalDetails");
        }
    }
}
