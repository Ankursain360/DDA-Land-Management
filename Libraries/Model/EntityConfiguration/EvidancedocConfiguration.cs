using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class EvidancedocConfiguration : IEntityTypeConfiguration<Evidancedoc>
    {
        public void Configure(EntityTypeBuilder<Evidancedoc> entity)
        {
            {
                entity.ToTable("evidancedoc", "lms");

                entity.HasIndex(e => e.ReqProcedingId)
                    .HasName("fk_RequestProceedingId_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.CreatedBy).HasColumnType("int(11)");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.DocName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DocPath)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EvidanceId).HasColumnType("int(11)");

                entity.Property(e => e.IsActive).HasColumnType("tinyint(4)");

                entity.Property(e => e.ModifiedBy).HasColumnType("int(11)");

                entity.Property(e => e.ReqProcedingId).HasColumnType("int(11)");

                entity.HasOne(d => d.ReqProceding)
                    .WithMany(p => p.Evidancedoc)
                    .HasForeignKey(d => d.ReqProcedingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkRequestProceedingId");
            }
        }
    }   }