using System.Linq;
using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Requests;
using Communications.Responses;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class ListTemplatesRequestConsumer: IConsumer<ListTemplatesRequest>
    {
        private readonly ITemplateMapper _mapper;
        private readonly ITemplateRepository _repository;

        public ListTemplatesRequestConsumer(ITemplateMapper mapper, ITemplateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ListTemplatesRequest> context)
        {
            var page = context.Message.Page > 0 ? context.Message.Page : 1;
            var pageSize = context.Message.PageSize <= 1 ? 1 : context.Message.PageSize;

            var (totalPages, templates) = await _repository.QueryByPage(
                page,
                pageSize,
                a => context.Message.ShowDeleted || !a.IsDeleted,
                a => a.Name);

            if (!templates.Any())
                await context.RespondAsync<TemplateNotFound>(new { });
            else
                await context.RespondAsync<ListTemplatesResponse>(new
                {
                    context.Message.Page,
                    context.Message.PageSize,
                    RecordsInPage = templates.Count,
                    TotalPages = totalPages,
                    Items = _mapper.MapModelToResponse(templates.ToList())
                });
        }
    }
}