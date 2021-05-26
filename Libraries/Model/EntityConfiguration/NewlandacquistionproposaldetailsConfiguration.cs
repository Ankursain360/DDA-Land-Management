using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class NewlandacquistionproposaldetailsConfiguration : IEntityTypeConfiguration<Newlandacquistionproposaldetails>
    {


        public void Configure(EntityTypeBuilder<Newlandacquistionproposaldetails> builder)
        {

            builder.ToTable("newlandacquistionproposaldetails", "lms");

            builder.HasIndex(e => e.SchemeId)
                .HasName("fknewSchemeId_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ProposalDate).HasColumnType("date");

            builder.Property(e => e.ProposalFileNo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.RequiredAgency)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.SchemeId).HasColumnType("int(11)");

            builder.HasOne(d => d.Scheme)
                .WithMany(p => p.Newlandacquistionproposaldetails)
                .HasForeignKey(d => d.SchemeId)
                .HasConstraintName("fknewSchemeId");
        }
    }
}
