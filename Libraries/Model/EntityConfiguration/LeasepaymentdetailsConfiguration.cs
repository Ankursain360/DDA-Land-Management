using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class LeasepaymentdetailsConfiguration : IEntityTypeConfiguration<Leasepaymentdetails>
    {
        public void Configure(EntityTypeBuilder<Leasepaymentdetails> builder)
        {
            builder.ToTable("leasepaymentdetails", "lms");

            builder.HasIndex(e => e.PaymentTypeId)
                .HasName("fkLeasePaymentType");

            builder.HasIndex(e => e.RefId)
                .HasName("fkLeaseAllotmentId");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.ChallanUtrnumber)
                .HasColumnName("ChallanUTRNumber")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.IsActive)
                .HasColumnType("tinyint(4)")
                .HasDefaultValueSql("1");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.PaymentAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PaymentDate).HasColumnType("date");

            builder.Property(e => e.PaymentMode)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.PaymentTypeId).HasColumnType("int(11)");

            builder.Property(e => e.RefId).HasColumnType("int(11)");

            builder.HasOne(d => d.PaymentType)
                .WithMany(p => p.Leasepaymentdetails)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("fkLeasePaymentType");

            builder.HasOne(d => d.Ref)
                .WithMany(p => p.Leasepaymentdetails)
                .HasForeignKey(d => d.RefId)
                .HasConstraintName("fkLeaseAllotmentId");
        }
    }
}