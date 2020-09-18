using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Libraries.Model.EntityConfiguration
{
   
   class DisposallandConfiguration : IEntityTypeConfiguration<Disposalland>
    {

        public void Configure(EntityTypeBuilder<Disposalland> builder)
        {
            builder.ToTable("disposalland", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AreaDisposed).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DateOfDisposed).HasColumnType("date");

            builder.Property(e => e.FileNoRefNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.KhasraId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Remarks)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.TransferBy)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.TransferTo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.TransferToWhichDept)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.UtilizationtypeId).HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");


        }
    }
}
