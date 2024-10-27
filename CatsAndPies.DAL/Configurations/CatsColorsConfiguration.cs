using CatsAndPies.Domain.Entities.Cats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Configurations
{
    public class CatsColorsConfiguration : IEntityTypeConfiguration<CatsColorEntity>
    {
        public void Configure(EntityTypeBuilder<CatsColorEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder
                .HasMany(x => x.Cats)
                .WithOne(c => c.Color)
                .HasForeignKey(x => x.ColorId);

            builder.HasData(
                new CatsColorEntity()
                {
                    Id = 1,
                    Name = "Белый"
                },
                new CatsColorEntity()
                {
                    Id = 2,
                    Name = "Черный"
                }, new CatsColorEntity()
                {
                    Id = 3,
                    Name = "Рыжий"
                }, new CatsColorEntity()
                {
                    Id = 4,
                    Name = "Серый"
                }, new CatsColorEntity()
                {
                    Id = 5,
                    Name = "Шоколадный"
                });
        }
    }
}
