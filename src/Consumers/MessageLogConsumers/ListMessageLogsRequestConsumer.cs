using System.Threading.Tasks;
using Communications.Requests;
using MassTransit;

namespace Communications.Business.Consumers.MessageLogConsumers
{
    public class ListMessageLogsRequestConsumer: IConsumer<ListMessageLogsRequest>
    {
        public Task Consume(ConsumeContext<ListMessageLogsRequest> context)
        {
            throw new System.NotImplementedException();
        }
    }
}