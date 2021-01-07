using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
 public  class ApprovalstatusConfiguration : IEntityTypeConfiguration<Approvalstatus>
    {
        public void Configure(EntityTypeBuilder<Approvalstatus> builder)
        {
            builder.ToTable("approvalstatus", "lms");

            builder.Property(e => e.IsActive).HasColumnType("int(11)");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");
            
            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
        }

        
    }
    
}
