using Broker.Requests;
using MassTransit;

namespace ServerSide.Consumers
{
    public class ReadLibraryConsumer : IConsumer<ReadLibraryRequest>
    {
        public Task Consume(ConsumeContext<ReadLibraryRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
