using System.Linq;
using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Requests;
using Communications.Responses;
using MassTransit;

namespace Communications.Business.Consumers.MessageLogConsumers
{
    public class ListMessageLogsRequestConsumer: IConsumer<ListMessageLogsRequest>
    {
        private readonly IMessageLogMapper _mapper;
        private readonly IMessageLogRepository _repository;

        public ListMessageLogsRequestConsumer(IMessageLogMapper mapper, IMessageLogRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ListMessageLogsRequest> context)
        {
            var page = context.Message.Page > 0 ? context.Message.Page : 1;
            var pageSize = context.Message.PageSize <= 1 ? 1 : context.Message.PageSize;

            var (totalPages, messageLogs) = await _repository.QueryByPage(
                page,
                pageSize,
                a => context.Message.ShowDeleted || !a.IsDeleted,
                a => a.SentAt);

            if (!messageLogs.Any())
                await context.RespondAsync<MessageLogNotFound>(new { });
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