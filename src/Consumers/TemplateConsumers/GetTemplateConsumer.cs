using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Requests;
using Communications.Responses;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class GetTemplateConsumer: IConsumer<GetTemplate>
    {
        private readonly ITemplateMapper _mapper;
        private readonly ITemplateRepository _repository;

        public GetTemplateConsumer(ITemplateMapper mapper, ITemplateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetTemplate> context)
        {
            var template = await _repository.FindById(context.Message.Id, context.Message.ShowDeleted);

            if (template is null)
                await context.RespondAsync<TemplateNotFound>(new { });
            else
                await context.RespondAsync(_mapper.MapModelToResponse(template));
        }
    }
}