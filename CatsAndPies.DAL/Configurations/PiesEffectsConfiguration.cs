using CatsAndPies.Domain.Entities.PiesTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.DAL.Configurations
{
    internal class PiesEffectsConfiguration : IEntityTypeConfiguration<PieEffectEntity>
    {
        public void Configure(EntityTypeBuilder<PieEffectEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .HasColumnType("text")
                .IsRequired();

            builder
                .HasOne(x => x.Rarity)
                .WithMany(x => x.Effects)
                .HasForeignKey(x => x.RarityId);
        }
    }
}
