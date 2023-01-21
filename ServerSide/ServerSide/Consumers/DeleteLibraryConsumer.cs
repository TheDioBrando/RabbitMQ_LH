using Broker.Requests;
using MassTransit;

namespace ServerSide.Consumers
{
    public class DeleteLibraryConsumer : IConsumer<DeleteLibraryRequest>
    {
        public Task Consume(ConsumeContext<DeleteLibraryRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
