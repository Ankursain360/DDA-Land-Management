using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
    public class LeaseApplicationConfiguration : IEntityTypeConfiguration<Leaseapplication>
    {
        public void Configure(EntityTypeBuilder<Leaseapplication> builder)
        {
            builder.ToTable("leaseapplication", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.Address).HasColumnType("longtext");

            builder.Property(e => e.ApprovedSataus).HasColumnType("int(11)");

            builder.Property(e => e.AreaSqlMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.ContactNo)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false);

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.Description).HasColumnType("longtext");

            builder.Property(e => e.EmailId)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.EstablishmentNameAddress).HasColumnType("longtext");

            builder.Property(e => e.FinancialPositionDescription).HasColumnType("longtext");

            builder.Property(e => e.FunctioningActivityUndertaken)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.FunctioningAreaSqMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.FunctioningSinceWhen)
                .HasMaxLength(45)
                .IsUnicode(false);

            builder.Property(e => e.IncomeTaxDescription).HasColumnType("longtext");

            builder.Property(e => e.IndemnityDescription).HasColumnType("longtext");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");

            builder.Property(e => e.LandAreaSqMt).HasColumnType("decimal(18,3)");

            builder.Property(e => e.LandAuthorisingDescription).HasColumnType("longtext");

            builder.Property(e => e.LandDetailsArea).HasColumnType("longtext");

            builder.Property(e => e.LandPurpose).HasColumnType("longtext");

            builder.Property(e => e.Locality)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Location1)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Location2)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Location3)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.NotarizedUndertakingDescription).HasColumnType("longtext");

            builder.Property(e => e.PendingAt).HasColumnType("int(11)");

            builder.Property(e => e.ProposedDescription).HasColumnType("longtext");

            builder.Property(e => e.Purpose).HasColumnType("longtext");

            builder.Property(e => e.Rate).HasColumnType("decimal(18,3)");

            builder.Property(e => e.RecommendationDescription).HasColumnType("longtext");

            builder.Property(e => e.RefNo)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.RefNoOfAllotmentLetterDate)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.RegistrationNo)
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.SponsorshipDescription).HasColumnType("longtext");
        }
    }
}
