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
    public class QuestionnairiesConfiguration : IEntityTypeConfiguration<QuestionnaireEntity>
    {
        public void Configure(EntityTypeBuilder<QuestionnaireEntity> builder)
        {
            builder.ToTable("Questionnaire");

            builder.HasKey(x => x.Id);
            builder.Property(q => q.Name).HasColumnType("varchar(100)");
            builder.Property(q => q.Birthday).HasColumnType("date");
            builder.Property(q => q.Season).HasColumnType("varchar(100)");
            builder.Property(q => q.Flower).HasColumnType("varchar(100)");
            builder.Property(q => q.Dish).HasColumnType("varchar(100)");
            builder.Property(q => q.Film).HasColumnType("varchar(100)");
            builder.Property(q => q.Singer).HasColumnType("varchar(100)");
            builder.Property(q => q.Color).HasColumnType("varchar(100)");
            builder.Property(q => q.Hobby).HasColumnType("text");
            builder.Property(q => q.ChillTime).HasColumnType("text");
            builder.Property(q => q.PositiveTraits).HasColumnType("text");
            builder.Property(q => q.Dream).HasColumnType("text");

            builder
                .HasOne(q => q.User)
                .WithOne(u => u.Questionnaire)
                .HasForeignKey<QuestionnaireEntity>(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
