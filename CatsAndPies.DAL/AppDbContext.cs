using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatsAndPies.Domain.Entities;
using CatsAndPies.DAL.Configurations;
using CatsAndPies.Domain.Entities.Cats;
using CatsAndPies.Domain.Entities.PiesTables;

namespace CatsAndPies.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<ExceptionLogEntity> Errors => Set<ExceptionLogEntity>();
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<WalletEntity> Wallets => Set<WalletEntity>();
        public DbSet<QuestionnaireEntity> Questionnairies => Set<QuestionnaireEntity>();
        public DbSet<CatEntity> Cats => Set<CatEntity>();
        public DbSet<CatsColorEntity> CatsColors => Set<CatsColorEntity>();
        public DbSet<CatsPersonalityEntity> CatsPersonalities => Set<CatsPersonalityEntity>();
        public DbSet<PieEntity> Pies => Set<PieEntity>();
        public DbSet<RarityEntity> Rarities => Set<RarityEntity>();
        public DbSet<PieEffectEntity> PiesEffects => Set<PieEffectEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExceptionLogsConfiguration());
            modelBuilder.ApplyConfiguration(new RolesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new WalletConfigurationEntity());
            modelBuilder.ApplyConfiguration(new QuestionnairiesConfiguration());
            modelBuilder.ApplyConfiguration(new CatsConfiguration());
            modelBuilder.ApplyConfiguration(new CatsColorsConfiguration());
            modelBuilder.ApplyConfiguration(new CatsPersonalitiesConfiguration());
            modelBuilder.ApplyConfiguration(new PiesConfiguration());
            modelBuilder.ApplyConfiguration(new RaritiesConfiguration());
            modelBuilder.ApplyConfiguration(new PiesEffectsConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
