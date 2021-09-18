namespace Communications.Business.Db
{
    using Abstractions;
    using Microsoft.Extensions.DependencyInjection;

    public static class Setup
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<ITemplateRepository, TemplateRepository>();
            services.AddScoped<IMessageLogRepository, MessageLogRepository>();

            return services;
        }
    }
}