using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class DemolitionstructurebeforedemolitionphotofiledetailsConfiguration : IEntityTypeConfiguration<Demolitionstructurebeforedemolitionphotofiledetails>
    {
        public void Configure(EntityTypeBuilder<Demolitionstructurebeforedemolitionphotofiledetails> builder)
        {
            builder.ToTable("demolitionstructurebeforedemolitionphotofiledetails", "lms");

            builder.HasIndex(e => e.DemolitionStructureId)
                .HasName("fkafterdemolitionphotodemoltionstructuredetails_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.BeforePhotoFilePath)
                            .IsRequired()
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemolitionStructureId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.HasOne(d => d.DemolitionStructureDetails)
                            .WithMany(p => p.Demolitionstructurebeforedemolitionphotofiledetails)
                            .HasForeignKey(d => d.DemolitionStructureId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fkafterdemolitionphotodemoltionstructuredetails");
        }
    }
}
