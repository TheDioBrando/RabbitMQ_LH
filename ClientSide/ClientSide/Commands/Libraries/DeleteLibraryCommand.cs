using Broker.Requests;
using Broker.Responses;
using ClientSide.Validation.Libraries;
using FluentValidation.Results;
using MassTransit;

namespace ServerSide.Commands.Libraries
{
    public interface IDeleteLibraryCommand
    {
        public Task<DeleteLibraryResponse> Execute(DeleteLibraryRequest request);
    }

    public class DeleteLibraryCommand : IDeleteLibraryCommand
    {
        IRequestClient<DeleteLibraryRequest> _requestClient;
        IDeleteLibraryRequestValidator _validator;

        public DeleteLibraryCommand(
            IRequestClient<DeleteLibraryRequest> requestClient,
            IDeleteLibraryRequestValidator validator)
        {
            _requestClient = requestClient;
            _validator = validator;
        }

        public async Task<DeleteLibraryResponse> Execute(DeleteLibraryRequest request)
        {
            ValidationResult result = _validator.Validate(request);

            if (!result.IsValid)
            {
                return new DeleteLibraryResponse
                {
                    Id = null,
                    IsSuccess = false,
                    Errors = result.Errors.Select(error => error.ErrorMessage).ToList()
                };
            }

            return (await _requestClient.GetResponse<DeleteLibraryResponse>(request)).Message;
        }
    }
}
