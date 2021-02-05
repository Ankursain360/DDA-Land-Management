using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Libraries.Model.EntityConfiguration
{
    public class DatastoragepartfilenodetailsConfiguration : IEntityTypeConfiguration<Datastoragepartfilenodetails>
    {
        public void Configure(EntityTypeBuilder<Datastoragepartfilenodetails> builder)
        {


            builder.ToTable("datastoragepartfilenodetails", "lms");

            builder.HasIndex(e => e.DataStorageDetailsId)
                .HasName("fk_DataStorageDetailsId_idx");

            builder.HasIndex(e => e.LocalityId)
                .HasName("fk_Locality_idx");

            builder.HasIndex(e => e.SchemeDptBranch)
                .HasName("fk_SchemeDptBranch_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Category)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.DataStorageDetailsId).HasColumnType("int(11)");

            builder.Property(e => e.Header)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.LocalityId).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.SchemeDptBranch).HasColumnType("int(11)");

            builder.Property(e => e.SequenceNo)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Subject)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.YearofPartFile).HasColumnType("int(11)");

            builder.HasOne(d => d.DataStorageDetails)
                .WithMany(p => p.Datastoragepartfilenodetails)
                .HasForeignKey(d => d.DataStorageDetailsId)
                .HasConstraintName("fk_DataStorageId");

            builder.HasOne(d => d.Locality)
                .WithMany(p => p.Datastoragepartfilenodetails)
                .HasForeignKey(d => d.LocalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Locality");

            builder.HasOne(d => d.SchemeDptBranchNavigation)
                .WithMany(p => p.Datastoragepartfilenodetails)
                .HasForeignKey(d => d.SchemeDptBranch)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SchemeDptBranch");

        }
    }

}
