using FluentValidation.Results;
using ClientSide.Validation.Libraries;
using Broker.Responses;
using Broker.Requests;
using MassTransit;

namespace ClientSide.Commands.Libraries
{
    public interface ICreateLibraryCommand
    {
        public Task<CreateLibraryResponse> Execute(CreateLibraryRequest request);
    }

    public class CreateLibraryCommand : ICreateLibraryCommand
    {
        private IRequestClient<CreateLibraryRequest> _requestClient;
        private CreateLibraryRequestValidator _validator;

        public CreateLibraryCommand(
            IRequestClient<CreateLibraryRequest> requestClient, 
            CreateLibraryRequestValidator validator)
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

            return _requestClient.GetResponse<CreateLibraryResponse>(request).Result.Message;
        }
    }
}
