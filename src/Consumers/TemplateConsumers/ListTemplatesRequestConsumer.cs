using System.Threading.Tasks;
using Communications.Requests;
using MassTransit;

namespace Communications.Business.Consumers.TemplateConsumers
{
    public class ListTemplatesRequestConsumer: IConsumer<ListTemplatesRequest>
    {
        public Task Consume(ConsumeContext<ListTemplatesRequest> context)
        {
            throw new System.NotImplementedException();
        }
    }
}