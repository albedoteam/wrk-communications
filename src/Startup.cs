using AlbedoTeam.Communications.Business.Mappers;
using AlbedoTeam.Sdk.DataLayerAccess;
using AlbedoTeam.Sdk.JobWorker.Configuration.Abstractions;
using AlbedoTeam.Sdk.MessageConsumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlbedoTeam.Communications.Business
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
            // services.AddRepositories();
            services.AddTransient<IJobRunner, JobConsumer>();

            // services.AddBroker(
            //     configure => configure
            //         .SetBrokerOptions(broker => broker.Host = configuration.GetValue<string>("BrokerOptions:Host")),
            //     consumers => consumers
            //         .Add<ListAccountsRequestConsumer>()
            //         .Add<GetAccountRequestConsumer>()
            //         .Add<CreateAccountRequestConsumer>()
            //         .Add<UpdateAccountRequestConsumer>()
            //         .Add<DeleteAccountRequestConsumer>(),
            //     queues => queues
            //         .Map<AccountUpdated>()
            //         .Map<AccountDeleted>());
        }
    }
}