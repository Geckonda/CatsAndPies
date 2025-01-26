using CatsAndPies.DAL.Repositories;
using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Abstractions.Repositories.Combined;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.Abstractions.Services.Cat;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Domain.Entities.PiesTables;
using CatsAndPies.Domain.Factories;
using CatsAndPies.Domain.Helpres;
using CatsAndPies.Domain.Helpres.Cache;
using CatsAndPies.Services.Implementations;
using CatsAndPies.Services.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace CatsAndPies
{
    public static class StartUpInit
    {
        public static void InitialiseRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<ExceptionLogEntity>, ExceptionLogRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
            services.AddScoped<ICatRepository, CatRepository>();
            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<IRarityRepository, RarityRepository>();
            services.AddScoped<IPieEffectRepository, PieEffectRepository>();
        }

        public static void InitialiseServices(this IServiceCollection services)
        {
            // Логгирование
            services.AddSingleton<LogQueueService>();
            // Сервис токенов
            services.AddScoped<ITokenService, TokenService>();
            // Сервисы связанные с аккаунтом
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IQuestionnaireService, QuestionnaireService>();

            // Сервисы кота
            services.AddScoped<ICatService, CatService>();

            // Сервисы пирожков
            services.AddScoped<IPieService, PieService>();
            
        }
        public static void InitialiseMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<QuestionnaireMappingProfile>();
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<WalletMappingProfile>();
                cfg.AddProfile<CatMappingProfile>();
                cfg.AddProfile<PieMappingProfile>();
            });
        }
        public static void InitialiseFactories(this IServiceCollection services)
        {
            services.AddSingleton<CatFactory>();
        }
        public static void InitialiseCaches(this IServiceCollection services)
        {
            //services.AddSingleton<RarityCache>();
            services.AddSingleton<RarityCache>(provider =>
            {
                using var scope = provider.CreateScope();
                var rarityRepository = scope.ServiceProvider.GetRequiredService<IRarityRepository>();
                var rarities = rarityRepository.GetAll().Result;
                return new RarityCache(rarities);
            });
            services.AddSingleton<PieEffectsCache>(provider =>
            {
                using var scope = provider.CreateScope();
                var effectRepository = scope.ServiceProvider.GetRequiredService<IPieEffectRepository>();
                var effects = effectRepository.GetAll().Result;
                return new PieEffectsCache(effects);
            });
            //services.AddSingleton<PieEffectsCache>();
        }
    }
}
