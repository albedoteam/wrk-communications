using System.Threading.Tasks;
using Communications.Requests;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class CreateTemplateRequestConsumer: IConsumer<CreateTemplateRequest>
    {
        public Task Consume(ConsumeContext<CreateTemplateRequest> context)
        {
            throw new System.NotImplementedException();
        }
    }
}