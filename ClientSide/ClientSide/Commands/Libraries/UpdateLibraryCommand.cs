using Broker.Requests;
using Broker.Responses;
using ClientSide.Validation.Libraries;
using FluentValidation.Results;
using MassTransit;

namespace ServerSide.Commands.Libraries
{
    public interface IUpdateLibraryCommand
    {
        public Task<UpdateLibraryResponse> Execute(UpdateLibraryRequest request);
    }

    public class UpdateLibraryCommand : IUpdateLibraryCommand
    {
        private IRequestClient<UpdateLibraryRequest> _requestClient;
        private IUpdateLibraryRequestValidator _validator;

        public UpdateLibraryCommand(
            IRequestClient<UpdateLibraryRequest> requestClient,
            IUpdateLibraryRequestValidator validator)
        {
            _requestClient = requestClient;
            _validator = validator;
        }

        public async Task<UpdateLibraryResponse> Execute(UpdateLibraryRequest request)
        {
            ValidationResult result = _validator.Validate(request);

            if (!result.IsValid)
            {
                return new UpdateLibraryResponse
                {
                    Id = null,
                    IsSuccess = false,
                    Errors = result.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            return (await _requestClient.GetResponse<UpdateLibraryResponse>(request)).Message;
        }
    }
}
