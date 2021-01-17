using System.Threading.Tasks;
using Communications.Requests;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class UpdateTemplateRequestConsumer: IConsumer<UpdateTemplateRequest>
    {
        public Task Consume(ConsumeContext<UpdateTemplateRequest> context)
        {
            throw new System.NotImplementedException();
        }
    }
}