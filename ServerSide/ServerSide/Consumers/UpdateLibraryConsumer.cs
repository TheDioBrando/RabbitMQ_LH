using Broker.Requests;
using MassTransit;

namespace ServerSide.Consumers
{
    public class UpdateLibraryConsumer : IConsumer<UpdateLibraryRequest>
    {
        public Task Consume(ConsumeContext<UpdateLibraryRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
