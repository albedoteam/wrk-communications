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

namespace Communications.Business.Consumers.TemplateConsumers
{
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

            var (totalPages, templates) = await _repository.QueryByPage(
                context.Message.AccountId,
                page,
                pageSize,
                filterBy,
                orderBy);

            if (!templates.Any())
                await context.RespondAsync<ErrorResponse>(new
                {
                    ErrorType = ErrorType.NotFound,
                    ErrorMessage = "Templates not found"
                });
            else
                await context.RespondAsync<ListTemplatesResponse>(new
                {
                    context.Message.Page,
                    context.Message.PageSize,
                    RecordsInPage = templates.Count,
                    TotalPages = totalPages,
                    Items = _mapper.MapModelToResponse(templates.ToList()),
                    context.Message.FilterBy,
                    context.Message.OrderBy,
                    context.Message.Sorting
                });
        }
        
        private FilterDefinition<Template> AddFilterBy(string filterBy)
        {
            if (string.IsNullOrWhiteSpace(filterBy))
                return null;

            var optionalFilters = Builders<Template>.Filter.Or(
                _repository.Helpers.Like(doc => doc.Name, filterBy)
            );

            return optionalFilters;
        }
    }
}