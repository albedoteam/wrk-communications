using System.Linq;
using System.Threading.Tasks;
using AlbedoTeam.Communications.Contracts.Common;
using AlbedoTeam.Communications.Contracts.Requests;
using AlbedoTeam.Communications.Contracts.Responses;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Business.Models;
using MassTransit;
using MongoDB.Driver;

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

            var filterBy = _repository.Helpers.CreateFilters(
                context.Message.AccountId,
                context.Message.ShowDeleted,
                null,
                AddFilterBy(context.Message.FilterBy));

            var orderBy = _repository.Helpers.CreateSorting(
                context.Message.OrderBy,
                context.Message.Sorting.ToString());

            var (totalPages, messageLogs) = await _repository.QueryByPage(
                context.Message.AccountId,
                page,
                pageSize,
                filterBy,
                orderBy);

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
                    Items = _mapper.MapModelToResponse(messageLogs.ToList()),
                    context.Message.FilterBy,
                    context.Message.OrderBy,
                    context.Message.Sorting
                });
        }
        
        private FilterDefinition<MessageLog> AddFilterBy(string filterBy)
        {
            if (string.IsNullOrWhiteSpace(filterBy))
                return null;

            var optionalFilters = Builders<MessageLog>.Filter.Or(
                _repository.Helpers.Like(a => a.From, filterBy),
                _repository.Helpers.Like(a => a.Subject, filterBy),
                _repository.Helpers.Like(a => a.Status, filterBy)
            );

            return optionalFilters;
        }
    }
}