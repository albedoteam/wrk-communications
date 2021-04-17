namespace Communications.Business.Consumers.TemplateConsumers
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

    public class ListTemplatesConsumer : IConsumer<ListTemplates>
    {
        private readonly ITemplateMapper _mapper;
        private readonly ITemplateRepository _repository;

        public ListTemplatesConsumer(ITemplateMapper mapper, ITemplateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ListTemplates> context)
        {
            var queryRequest = QueryUtils.GetQueryParams<Template>(_mapper.RequestToQuery(context.Message));
            var queryResponse = await _repository.QueryByPage(context.Message.AccountId, queryRequest);

            if (!queryResponse.Records.Any())
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = "Templates not found"
                });
            else
                await context.RespondAsync<ListTemplatesResponse>(new
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