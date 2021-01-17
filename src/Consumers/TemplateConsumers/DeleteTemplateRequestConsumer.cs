using System.Threading.Tasks;
using Communications.Requests;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class DeleteTemplateRequestConsumer: IConsumer<DeleteTemplateRequest>
    {
        public Task Consume(ConsumeContext<DeleteTemplateRequest> context)
        {
            throw new System.NotImplementedException();
        }
    }
}