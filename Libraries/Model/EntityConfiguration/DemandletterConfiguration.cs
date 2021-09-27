
using System;
using System.Collections.Generic;
using System.Text;
using Libraries.Model.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libraries.Model.EntityConfiguration
{
   public class DemandletterConfiguration : IEntityTypeConfiguration<Demandletter>
    {
        public void Configure(EntityTypeBuilder<Demandletter> builder)
        {



            //builder.ToTable("demandletter", "lms");

            builder.Property(e => e.Id).HasColumnType("int(11)");

            builder.Property(e => e.BalanceAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.CreatedBy).HasColumnType("int(11)");

            builder.Property(e => e.CreatedDate).HasColumnType("date");

            builder.Property(e => e.DemandDate).HasColumnType("date");

            builder.Property(e => e.DemandLetterFilePath).HasColumnType("longtext");

            builder.Property(e => e.DemandNumber).HasColumnType("int(11)");

            builder.Property(e => e.DemandPeriod).HasColumnType("int(11)");

            builder.Property(e => e.DueDate).HasColumnType("date");

            builder.Property(e => e.FileNo).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedBy).HasColumnType("int(11)");

            builder.Property(e => e.ModifiedDate).HasColumnType("date");

            builder.Property(e => e.OutStandingAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PaidAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.PreviousBalanceAmount).HasColumnType("decimal(18,3)");

            builder.Property(e => e.IsActive).HasColumnType("tinyint(4)");







        }
    }
    }
