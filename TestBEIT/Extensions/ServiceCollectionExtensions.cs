using TestBEIT.Logics.Students;

namespace TestBEIT.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLogic(this IServiceCollection services)
        {
            services.AddScoped<SiswaLogic>();
            return services;
        }
    }
}
