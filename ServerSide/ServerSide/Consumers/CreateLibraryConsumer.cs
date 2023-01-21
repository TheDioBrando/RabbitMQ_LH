using Broker.Requests;
using Broker.Responses;
using MassTransit;
using ServerSide.Data.Repositories;
using ServerSide.Mappers;

namespace ServerSide.Consumers
{
    public class CreateLibraryConsumer : IConsumer<CreateLibraryRequest>
    {
        private ILibraryRepository _repository;
        private IDbLibraryMapper _mapper;

        public CreateLibraryConsumer( 
            ILibraryRepository repository,
            IDbLibraryMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<CreateLibraryRequest> context)
        {
            CreateLibraryResponse response = new()
            {
                Id= _repository.Create(_mapper.Map(context.Message)).Result,
                IsSuccess = true
            };

            return context.RespondAsync<CreateLibraryResponse>(response);
        }
    }
}
