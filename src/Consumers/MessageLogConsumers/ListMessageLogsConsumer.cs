using System.Linq;
using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using MassTransit;

namespace Communications.Business.Consumers.MessageLogConsumers
{
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
            var page = context.Message.Page > 0 ? context.Message.Page : 1;
            var pageSize = context.Message.PageSize <= 1 ? 1 : context.Message.PageSize;

            var (totalPages, messageLogs) = await _repository.QueryByPage(
                page,
                pageSize,
                a => context.Message.ShowDeleted || !a.IsDeleted,
                a => a.SentAt);

            if (!messageLogs.Any())
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = "Message logs not found"
                });
            else
                await context.RespondAsync<ListMessageLogsResponse>(new
                {
                    context.Message.Page,
                    context.Message.PageSize,
                    RecordsInPage = messageLogs.Count,
                    TotalPages = totalPages,
                    Items = _mapper.MapModelToResponse(messageLogs.ToList())
                });
        }
    }
}