using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class HearingdetailsConfiguration : IEntityTypeConfiguration<Hearingdetails>
    {
        public void Configure(EntityTypeBuilder<Hearingdetails> entity)
        {
            {
                entity.ToTable("hearingdetails", "lms");

                entity.HasIndex(e => e.EvidanceDocId)
                    .HasName("fk_EvidanceDocId");

                entity.HasIndex(e => e.NoticeGenId)
                    .HasName("fk_NoticeGenId");

                entity.HasIndex(e => e.ReqProcId)
                    .HasName("fk_ReqProcId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Attendee)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.EvidanceDocId).HasColumnType("int(11)");

                entity.Property(e => e.HearingDate).HasColumnType("date");

                entity.Property(e => e.HearingVenue)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

                entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

                entity.Property(e => e.NoticeGenId).HasColumnType("int(11)");

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ReqProcId).HasColumnType("int(11)");

                entity.HasOne(d => d.EvidanceDoc)
                    .WithMany(p => p.Hearingdetails)
                    .HasForeignKey(d => d.EvidanceDocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_EvidanceDocId");

                entity.HasOne(d => d.NoticeGen)
                    .WithMany(p => p.Hearingdetails)
                    .HasForeignKey(d => d.NoticeGenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_NoticeGenId");

                entity.HasOne(d => d.ReqProc)
                    .WithMany(p => p.Hearingdetails)
                    .HasForeignKey(d => d.ReqProcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ReqProcId");
            }
        }
    }
}
