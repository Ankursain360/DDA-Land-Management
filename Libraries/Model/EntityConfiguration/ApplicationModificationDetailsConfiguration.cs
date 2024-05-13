using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
    public class ApplicationModificationDetailsConfiguration : IEntityTypeConfiguration<ApplicationModificationDetails>
    {
        public void Configure(EntityTypeBuilder<ApplicationModificationDetails> builder)
        {
            builder.Property(e => e.moduleName)
               .IsRequired()
               .HasMaxLength(500)
               .IsUnicode(false);

            builder.Property(e=>e.changeLog)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.updated).HasColumnType("datetime");

            builder.Property(e => e.Isactive).HasColumnType("tinyint(4)");

           // builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

           // builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.Property(e=>e.UpdatedBy)
                .HasMaxLength(100)
                .IsUnicode(false);
           
        }
    }
}
