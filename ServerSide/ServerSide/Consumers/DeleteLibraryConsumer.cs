using Broker.Requests;
using Broker.Responses;
using MassTransit;
using ServerSide.Data.Repositories;

namespace ServerSide.Consumers
{
    public class DeleteLibraryConsumer : IConsumer<DeleteLibraryRequest>
    {
        private ILibraryRepository _repository;

        public DeleteLibraryConsumer(ILibraryRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<DeleteLibraryRequest> context)
        {
            DeleteLibraryResponse response = new()
            {
                Id = _repository.Delete(context.Message).Result,
                IsSuccess = true
            };

            return context.RespondAsync<DeleteLibraryResponse>(response);
        }
    }
}
