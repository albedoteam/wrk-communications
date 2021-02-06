using AlbedoTeam.Accounts.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Events;
using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.JobWorker.Configuration.Abstractions;
using AlbedoTeam.Sdk.MessageConsumer;
using Communications.Business.Consumers;
using Communications.Business.Consumers.ConfigurationConsumers;
using Communications.Business.Consumers.MessageLogConsumers;
using Communications.Business.Consumers.TemplateConsumers;
using Communications.Business.Db;
using Communications.Business.Mappers;
using Communications.Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Communications.Business
{
    public class Startup : IWorkerConfigurator
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataLayerAccess(db =>
            {
                db.ConnectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
                db.DatabaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            });

            services.AddMappers();
            services.AddRepositories();
            services.AddServices();
            services.AddTransient<IJobRunner, JobConsumer>();

            services.AddBroker(
                configure => configure
                    .SetBrokerOptions(broker => broker.Host = configuration.GetValue<string>("BrokerOptions:Host")),
                consumers =>
                {
                    // configurations
                    consumers
                        .Add<ListConfigurationsConsumer>()
                        .Add<GetConfigurationConsumer>()
                        .Add<CreateConfigurationConsumer>()
                        .Add<UpdateConfigurationsConsumer>()
                        .Add<DeleteConfigurationConsumer>();

                    // message logs
                    consumers
                        .Add<ListMessageLogsConsumer>();

                    // templates
                    consumers
                        .Add<ListTemplatesConsumer>()
                        .Add<GetTemplateConsumer>()
                        .Add<CreateTemplateConsumer>()
                        .Add<UpdateTemplateConsumer>()
                        .Add<DeleteTemplateConsumer>();

                    // message sender
                    consumers
                        .Add<SendMessageConsumer>();
                },
                queues => queues
                    .Map<MessageSent>(),
                clients => clients
                    .Add<GetAccount>());
        }
    }
}