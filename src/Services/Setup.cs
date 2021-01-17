using Communications.Business.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Communications.Business.Services
{
    public static class Setup
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}