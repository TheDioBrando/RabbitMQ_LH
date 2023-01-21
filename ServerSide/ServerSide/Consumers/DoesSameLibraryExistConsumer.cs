using Broker.Requests;
using Broker.Responses;
using MassTransit;
using ServerSide.Data.Repositories;

namespace ServerSide.Consumers
{
    public class DoesSameLibraryExistConsumer : IConsumer<DoesSameLibraryExistRequest>
    {
        private ILibraryRepository _repository;

        public DoesSameLibraryExistConsumer(ILibraryRepository repository)
        {
            _repository = repository;
        }

        public Task Consume(ConsumeContext<DoesSameLibraryExistRequest> context)
        {
            DoesSameLibraryExistResponse response = new()
            {
                IsExist = _repository.DoesSameAddressExist(context.Message.Address)
            };

            return context.RespondAsync<DoesSameLibraryExistResponse>(response);
        }
    }
}
