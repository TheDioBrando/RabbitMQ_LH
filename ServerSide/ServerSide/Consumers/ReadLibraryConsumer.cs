using Broker.Requests;
using Broker.Responses;
using MassTransit;
using ServerSide.Data.Repositories;

namespace ServerSide.Consumers
{
    public class ReadLibraryConsumer : IConsumer<ReadLibraryRequest>
    {
        private ILibraryRepository _repository;

        public ReadLibraryConsumer(ILibraryRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<ReadLibraryRequest> context)
        {
            ReadLibrariesResponse response = new()
            {
                Addresses = _repository.Read().Select(lib => lib.Address).ToList()
            };

            return context.RespondAsync<ReadLibrariesResponse>(response);
        }
    }
}
