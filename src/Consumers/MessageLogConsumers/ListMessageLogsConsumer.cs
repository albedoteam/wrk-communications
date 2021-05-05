namespace Communications.Business.Consumers.MessageLogConsumers
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

    public class ListMessageLogsConsumer : IConsumer<ListMessageLogs>
    {
        private readonly IMessageLogMapper _mapper;
        private readonly IMessageLogRepository _repository;

        public ListMessageLogsConsumer(IMessageLogMapper mapper, IMessageLogRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ListMessageLogs> context)
        {
            var queryRequest = QueryUtils.GetQueryParams<MessageLog>(_mapper.RequestToQuery(context.Message));
            var queryResponse = await _repository.QueryByPage(context.Message.AccountId, queryRequest);

            await context.RespondAsync<ListMessageLogsResponse>(new
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