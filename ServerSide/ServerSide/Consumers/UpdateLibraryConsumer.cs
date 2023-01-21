using Broker.Requests;
using Broker.Responses;
using MassTransit;
using ServerSide.Data.Repositories;

namespace ServerSide.Consumers
{
    public class UpdateLibraryConsumer : IConsumer<UpdateLibraryRequest>
    {
        private ILibraryRepository _repository;

        public UpdateLibraryConsumer(ILibraryRepository repository)
        {
             _repository = repository;
        }

        public Task Consume(ConsumeContext<UpdateLibraryRequest> context)
        {
            UpdateLibraryResponse response = new()
            {
                Id = _repository.Update(context.Message).Result,
                IsSuccess = true
            };

            return context.RespondAsync<UpdateLibraryResponse>(response);
        }
    }
}
