using FluentValidation.Results;
using ClientSide.Validation.Libraries;
using Broker.Responses;
using Broker.Requests;
using MassTransit;
using System.Runtime.CompilerServices;

namespace ClientSide.Commands.Libraries
{
    public interface ICreateLibraryCommand
    {
        public Task<CreateLibraryResponse> Execute(CreateLibraryRequest request);
    }

    public class CreateLibraryCommand : ICreateLibraryCommand
    {
        private IRequestClient<CreateLibraryRequest> _requestClient;
        private ICreateLibraryRequestValidator _validator;

        public CreateLibraryCommand(
            IRequestClient<CreateLibraryRequest> requestClient, 
            ICreateLibraryRequestValidator validator)
        {
            _requestClient = requestClient;
            _validator = validator;
        }

        public async Task<CreateLibraryResponse> Execute(CreateLibraryRequest request)
        {
            ValidationResult validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return new CreateLibraryResponse()
                {
                    Id = null,
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(er => er.ErrorMessage).ToList()
                };
            }

            return (await _requestClient.GetResponse<CreateLibraryResponse>(request)).Message;
        }
    }
}
