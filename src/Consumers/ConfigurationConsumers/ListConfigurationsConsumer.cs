namespace Communications.Business.Consumers.ConfigurationConsumers
{
    using System.Linq;
    using System.Threading.Tasks;
    using AlbedoTeam.Communications.Contracts.Common;
    using AlbedoTeam.Communications.Contracts.Requests;
    using AlbedoTeam.Communications.Contracts.Responses;
    using AlbedoTeam.Sdk.DataLayerAccess.Utils.Query;
    using Db.Abstractions;
    using Mappers.Abstractions;
    using MassTransit;
    using Models;

    public class ListConfigurationsConsumer : IConsumer<ListConfigurations>
    {
        private readonly IConfigurationMapper _mapper;
        private readonly IConfigurationRepository _repository;

        public ListConfigurationsConsumer(IConfigurationMapper mapper, IConfigurationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ListConfigurations> context)
        {
            var queryRequest = QueryUtils.GetQueryParams<Configuration>(_mapper.RequestToQuery(context.Message));
            var queryResponse = await _repository.QueryByPage(context.Message.AccountId, queryRequest);

            await context.RespondAsync<ListConfigurationsResponse>(new
            {
                queryResponse.Page,
                queryResponse.PageSize,
                queryResponse.RecordsInPage,
                queryResponse.TotalPages,
                Items = _mapper.MapModelToResponse(queryResponse.Records.ToList()),
                context.Message.FilterBy,
                context.Message.OrderBy,
                context.Message.Sorting
            });
        }
    }
}