using CatsAndPies.Domain.Entities.PiesTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Configurations
{
    public class RaritiesConfiguration : IEntityTypeConfiguration<RarityEntity>
    {
        public void Configure(EntityTypeBuilder<RarityEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Chance)
                .HasColumnType("DOUBLE PRECISION")
                .IsRequired();

            builder
                .HasMany(x => x.Effects)
                .WithOne(x => x.Rarity)
                .HasForeignKey(x => x.RarityId);

            builder.HasData(
                new RarityEntity()
                {
                    Id = 1,
                    Name = "Обычный",
                    Chance = 60
                },
                new RarityEntity()
                { 
                    Id = 2,
                    Name = "Необычный",
                    Chance = 25
                },
                new RarityEntity()
                {
                    Id = 3,
                    Name = "Редкий",
                    Chance = 10
                },
                new RarityEntity()
                {
                    Id = 4,
                    Name = "Эпический",
                    Chance = 4
                },
                 new RarityEntity()
                 {
                     Id = 5,
                     Name = "Легендарный",
                     Chance = 0.9
                 },
                 new RarityEntity()
                 {
                     Id = 6,
                     Name = "Мифический",
                     Chance = 0.1
                 }
                );
        }
    }
}
