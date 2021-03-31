using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Libraries.Model.EntityConfiguration
{
    public class HearingdetailsConfiguration : IEntityTypeConfiguration<Hearingdetails>
    {
        public void Configure(EntityTypeBuilder<Hearingdetails> builder)
        {
           
                builder.ToTable("hearingdetails", "lms");

                builder.HasIndex(e => e.ReqProcId)
                    .HasName("fk_ReqProcId");

                builder.Property(e => e.Id).HasColumnType("int(11)");

                builder.Property(e => e.Attendee)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

                builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                builder.Property(e => e.DocumentPatth).HasColumnType("longtext");

                builder.Property(e => e.HearingDate).HasColumnType("date");

                builder.Property(e => e.HearingVenue)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

                builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

                builder.Property(e => e.Remark)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                builder.Property(e => e.ReqProcId).HasColumnType("int(11)");

                builder.HasOne(d => d.ReqProc)
                    .WithMany(p => p.Hearingdetails)
                    .HasForeignKey(d => d.ReqProcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ReqProcId");
            }
        }
    }

