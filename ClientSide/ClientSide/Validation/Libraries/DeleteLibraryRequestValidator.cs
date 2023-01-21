using FluentValidation;
using Broker.Requests;
using MassTransit;
using Broker.Responses;
using FluentValidation.Results;

namespace ClientSide.Validation.Libraries
{
    public interface IDeleteLibraryRequestValidator
    {
        public ValidationResult Validate(DeleteLibraryRequest request);
    }

    public class DeleteLibraryRequestValidator : AbstractValidator<DeleteLibraryRequest>, IDeleteLibraryRequestValidator
    {
        public DeleteLibraryRequestValidator(IRequestClient<DoesSameLibraryExistRequest> requestClient)
        {
            RuleFor(request => request.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Address can't be empty")
                .Must(address =>
                {
                    var existRequest = new DoesSameLibraryExistRequest
                    {
                        Address = address
                    };

                    return requestClient.GetResponse<DoesSameLibraryExistResponse>(existRequest).Result.Message.IsExist;
                })
                .WithMessage("This address doesn't exist");
        }
    }
}
