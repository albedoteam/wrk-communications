namespace Communications.Business.Services
{
    using Abstractions;
    using Microsoft.Extensions.DependencyInjection;

    public static class Setup
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICommunicationService, CommunicationService>();

            return services;
        }
    }
}