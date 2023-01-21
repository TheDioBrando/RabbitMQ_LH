using BrokerRequests;
using MassTransit;

namespace ServerSide.Consumers
{
    public class PostUserConsumer : IConsumer<PostUserRequest>
    {
        public Task Consume(ConsumeContext<PostUserRequest> context)
        {
            BrokerResponse resp = new BrokerResponse();
            resp.IsSuccess = true;

            return context.RespondAsync<BrokerResponse>(resp);
        }
    }
}
