using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{

    public class AllotmentletterConfiguration : IEntityTypeConfiguration<Allotmentletter>
    {
        public void Configure(EntityTypeBuilder<Allotmentletter> builder)
        {
            {
                //builder.ToTable("allotmentletter", "lms");

                builder.HasIndex(e => e.AllotmentId)
                    .HasName("fk_LeaseAllotmentId");

                builder.Property(e => e.Id).HasColumnType("int(11)");

                builder.Property(e => e.AllotmentId).HasColumnType("int(11)");

                builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

                builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                builder.Property(e => e.DemandAmount).HasColumnType("decimal(18,3)");


                builder.Property(e => e.DemandDate).HasColumnType("date");

                builder.Property(e => e.DemandPeriodEnd).HasColumnType("date");

                builder.Property(e => e.DemandPeriodStart).HasColumnType("date");

                builder.Property(e => e.FeeTypeId).HasColumnType("int(11)");

                builder.Property(e => e.FilePath).HasColumnType("longtext");

                builder.Property(e => e.IsActive)
                            .HasColumnType("tinyint(4)")
                            .HasDefaultValueSql("1");

                builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

                builder.Property(e => e.ReferenceNumber)
                            .IsRequired()
                            .HasMaxLength(200)
                            .IsUnicode(false);

                builder.HasOne(d => d.Allotment)
                            .WithMany(p => p.Allotmentletter)
                            .HasForeignKey(d => d.AllotmentId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fk_LeaseAllotmentId");
            }
        }
    }

    }
