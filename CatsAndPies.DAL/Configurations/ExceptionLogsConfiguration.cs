using CatsAndPies.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Configurations
{
    public class ExceptionLogsConfiguration : IEntityTypeConfiguration<ExceptionLogEntity>
    {
        public void Configure(EntityTypeBuilder<ExceptionLogEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExceptionTime)
                .HasColumnType("timestamp") // Указываем тип TIMESTAMP без временной зоны
                .IsRequired();

            builder.Property(x => x.ExceptionType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Method)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ExceptionText)
                .IsRequired()
                .HasMaxLength(1000);
            

            builder.ToTable("ExceptionLogs");

            builder.HasIndex(e => e.ExceptionTime);
        }
    }
}
