using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class AwardmasterdetailConfiguration : IEntityTypeConfiguration<Awardmasterdetail>
    {
        public void Configure(EntityTypeBuilder<Awardmasterdetail> builder)
        {
            builder.ToTable("awardmasterdetail", "lms");

            builder.HasIndex(e => e.VillageId)
               .HasName("VillageId_idx");

            builder.HasIndex(e => e.ProposalId)
                .HasName("PurposalId_idx");

            builder.HasIndex(e => e.Id)
                .HasName("Id_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.Us6id)
                .HasName("US6Id_idx");

            builder.HasIndex(e => e.Us4id)
                .HasName("US4Id_idx");

            builder.HasIndex(e => e.Us17id)
               .HasName("US17Id_idx");

            builder.HasIndex(e => e.Compensation)
               .HasName("Compensation_idx");

            
            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AwardDate).HasColumnType("date");

            builder.Property(e => e.AwardNumber)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Compensation)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.Rate1)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Rate2)
                .HasMaxLength(100)
                .IsUnicode(false);
            builder.Property(e => e.Rate3)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Rate4)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Nature)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ProposalId).HasColumnType("int(11)");

            builder.Property(e => e.Purpose)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Remarks)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Us17id)
                .HasColumnName("US17Id")
                .HasColumnType("int(11)");

            builder.Property(e => e.Us4id)
                .HasColumnName("US4Id")
                .HasColumnType("int(11)");

            builder.Property(e => e.Us6id)
                .HasColumnName("US6Id")
                .HasColumnType("int(11)");

            builder.Property(e => e.VillageId).HasColumnType("int(11)");

             builder.HasOne(d => d.Proposal)
                .WithMany(p => p.Awardmasterdetail)
                .HasForeignKey(d => d.ProposalId)
                .HasConstraintName("fk_ProposalId");

            builder.HasOne(d => d.Us6)
                .WithMany(p => p.Awardmasterdetail)
                .HasForeignKey(d => d.Us6id)
                .HasConstraintName("fk_US6Id");

            builder.HasOne(d => d.Us4)
                .WithMany(p => p.Awardmasterdetail)
                .HasForeignKey(d => d.Us4id)
                .HasConstraintName("fk_US4Id");

            builder.HasOne(d => d.Us17)
                .WithMany(p => p.Awardmasterdetail)
                .HasForeignKey(d => d.Us17id)
                .HasConstraintName("fk_US17Id");

            builder.HasOne(d => d.Acquiredlandvillage)
                .WithMany(p => p.Awardmasterdetail)
                .HasForeignKey(d => d.VillageId)
                .HasConstraintName("fk_acqVillageId");

        }

    }
}
