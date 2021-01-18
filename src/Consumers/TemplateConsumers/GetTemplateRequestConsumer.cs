using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Requests;
using Communications.Responses;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class GetTemplateRequestConsumer: IConsumer<GetTemplateRequest>
    {
        private readonly ITemplateMapper _mapper;
        private readonly ITemplateRepository _repository;

        public GetTemplateRequestConsumer(ITemplateMapper mapper, ITemplateRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<GetTemplateRequest> context)
        {
            var template = await _repository.FindById(context.Message.Id, context.Message.ShowDeleted);

            if (template is null)
                await context.RespondAsync<TemplateNotFound>(new { });
            else
                await context.RespondAsync(_mapper.MapModelToResponse(template));
        }
    }
}