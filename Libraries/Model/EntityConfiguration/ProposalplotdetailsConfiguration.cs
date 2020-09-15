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

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Bigha).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswa).HasColumnType("decimal(18,3)");

            builder.Property(e => e.Biswanshi).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ProposaldetailsId).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");


        }
     
     }
}