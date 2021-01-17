using System.Threading.Tasks;
using Communications.Business.Db.Abstractions;
using Communications.Business.Mappers.Abstractions;
using Communications.Requests;
using Communications.Responses;
using MassTransit;

namespace Communications.Business.Consumers.ConfigurationConsumers
{
    public class DeleteConfigurationRequestConsumer: IConsumer<DeleteConfigurationRequest>
    {
        private readonly IConfigurationMapper _mapper;
        private readonly IConfigurationRepository _repository;

        public DeleteConfigurationRequestConsumer(IConfigurationMapper mapper, IConfigurationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<DeleteConfigurationRequest> context)
        {
            var configuration = await _repository.FindById(context.Message.Id);
            if (configuration is null)
            {
                await context.RespondAsync<ConfigurationNotFound>(new { });
            }
            else
            {
                await _repository.DeleteById(context.Message.Id);

                // get "soft-deleted"
                configuration = await _repository.FindById(context.Message.Id, true);

                await context.RespondAsync(_mapper.MapModelToResponse(configuration)); // respond async
            }
        }
    }
}