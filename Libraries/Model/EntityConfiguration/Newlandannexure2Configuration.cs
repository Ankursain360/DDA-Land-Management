using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class Newlandannexure2Configuration : IEntityTypeConfiguration<Newlandannexure2>
    {
        public void Configure(EntityTypeBuilder<Newlandannexure2> builder)
        {
            builder.ToTable("newlandannexure2", "lms");

            builder.HasIndex(e => e.ReqId)
                .HasName("ReqId_UNIQUE")
                .IsUnique();

            builder.Property(e => e.ReqId).HasColumnType("int(11)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.OfficialDesigOfReqBody)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.OtherRemarks)
                .HasMaxLength(1000)
                .IsUnicode(false);

            builder.Property(e => e.ProjectCost).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ProjectMonth)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.ProjectYear)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PurposeOfAcqDetails)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.ReqBodyType)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Sn10val)
                .HasColumnName("SN10val")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sn11val)
                .HasColumnName("SN11Val")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sn12filePath)
                .HasColumnName("SN12FilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.Sn1Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Sn1val)
                .HasColumnName("SN1Val")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sn2Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Sn2val)
                .HasColumnName("SN2Val")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sn3Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Sn3val)
                .HasColumnName("SN3Val")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sn4val)
                .HasColumnName("SN4Val")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sn5Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Sn5val)
                .HasColumnName("SN5Val")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sn6val)
                .HasColumnName("SN6Val")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sn7File).HasColumnType("longtext");

            builder.Property(e => e.Sn7Remark)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Sn7val)
                .HasColumnName("SN7Val")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Sn8date).HasColumnName("SN8Date");

            builder.Property(e => e.Sn8filePath)
                .HasColumnName("SN8FilePath")
                .HasColumnType("longtext");

            builder.Property(e => e.Sn8remarks)
                .HasColumnName("SN8Remarks")
                .HasMaxLength(2000)
                .IsUnicode(false);

            builder.Property(e => e.Sn9date).HasColumnName("SN9Date");

            builder.Property(e => e.Sn9filePath)
                .HasColumnName("SN9FilePath")
                .HasColumnType("longtext");


        }
    }
}
