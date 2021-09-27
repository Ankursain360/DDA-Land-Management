using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Model.EntityConfiguration
{
    public class DemolitionstructureafterdemolitionphotofiledetailsConfiguration : IEntityTypeConfiguration<Demolitionstructureafterdemolitionphotofiledetails>
    {
        public void Configure(EntityTypeBuilder<Demolitionstructureafterdemolitionphotofiledetails> builder)
        {
            //builder.ToTable("demolitionstructureafterdemolitionphotofiledetails", "lms");

            builder.HasIndex(e => e.DemolitionStructureDetailsId)
                .HasName("fkdemolitionstructuredetailsafterphoto_idx");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.AfterPhotoFilePath)
                            .IsRequired()
                            .HasMaxLength(1000)
                            .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.DemolitionStructureDetailsId).HasColumnType("int(11)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.HasOne(d => d.DemolitionStructureDetails)
                            .WithMany(p => p.Demolitionstructureafterdemolitionphotofiledetails)
                            .HasForeignKey(d => d.DemolitionStructureDetailsId)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("fkdemolitionstructuredetailsafterphoto");
        }
    }
}


