using System.Threading.Tasks;
using Communications.Requests;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class GetTemplateRequestConsumer: IConsumer<GetTemplateRequest>
    {
        public Task Consume(ConsumeContext<GetTemplateRequest> context)
        {
            throw new System.NotImplementedException();
        }
    }
}