using Broker.Requests;
using Broker.Responses;
using MassTransit;

namespace ServerSide.Commands.Libraries
{
    public interface IReadLibraryCommand
    {
        public Task<ReadLibrariesResponse> Execute(ReadLibraryRequest request);
    }

    public class ReadLibraryCommand : IReadLibraryCommand
    {
        private IRequestClient<ReadLibraryRequest> _requestClient;

        public ReadLibraryCommand(IRequestClient<ReadLibraryRequest> requestClient)
        {
            _requestClient = requestClient;
        }

        public async Task<ReadLibrariesResponse> Execute(ReadLibraryRequest request)
        {
            return (await _requestClient.GetResponse<ReadLibrariesResponse>(request)).Message;
        }
    }
}
