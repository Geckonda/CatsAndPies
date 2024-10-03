using CatsAndPies.DAL.Repositories;
using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Abstractions.Services;
using CatsAndPies.Domain.Entities;
using CatsAndPies.Services.Implementations;
using CatsAndPies.Services.Mapping;

namespace CatsAndPies
{
    public static class StartUpInit
    {
        public static void InitialiseRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<UserEntity>, UserRepository>();
            services.AddScoped<IBaseRepository<QuestionnaireEntity>, QuestionnaireRepository>();
        }

        public static void InitialiseServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IQuestionnaireService, QuestionnaireService>();
        }
        public static void InitialiseMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<QuestionnaireMappingProfile>();
            });
        }
    }
}
