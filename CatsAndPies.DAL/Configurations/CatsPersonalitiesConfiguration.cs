using CatsAndPies.Domain.Entities;
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
    public class CatsPersonalitiesConfiguration : IEntityTypeConfiguration<CatsPersonalityEntity>
    {
        public void Configure(EntityTypeBuilder<CatsPersonalityEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("text")
                .IsRequired();

            builder
                .HasMany(x => x.Cats)
                .WithOne(c => c.Personality)
                .HasForeignKey(x => x.PersonalityId);

            builder.HasData(
                new CatsPersonalityEntity
                {
                    Id = 1,
                    Name = "Заносчивый",
                    Description = "Считает себя лучше всех и не упустит момента показать свое превосходство. Часто саркастичен, но с оттенком высокомерия."
                },
                new CatsPersonalityEntity
                {
                    Id = 2,
                    Name = "Мудрый",
                    Description = "Этот кот всегда говорит философским тоном, любит поучать и делиться своей «мудростью». Возможно, его реплики звучат как древние пословицы или загадки."
                },
                new CatsPersonalityEntity
                {
                    Id = 3,
                    Name = "Любопытный",
                    Description = "Постоянно задает вопросы, интересуется всем подряд, будто бы видит все впервые. В его репликах часто встречаются удивление и восторг."
                });
        }
    }
}
