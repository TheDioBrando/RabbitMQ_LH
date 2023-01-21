using FluentValidation;
using Broker.Requests;
using MassTransit;
using Broker.Responses;
using FluentValidation.Results;

namespace ClientSide.Validation.Libraries
{
    public interface ICreateLibraryRequestValidator
    {
        public ValidationResult Validate(CreateLibraryRequest request);
    }

    public class CreateLibraryRequestValidator : AbstractValidator<CreateLibraryRequest>, ICreateLibraryRequestValidator
    {
        public CreateLibraryRequestValidator(IRequestClient<DoesSameLibraryExistRequest> requestClient)
        {
            RuleFor(request => request.Address)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Address can't be empty")
                .MinimumLength(2)
                .WithMessage("Address is too short")
                .MaximumLength(30)
                .WithMessage("Address is too long")
                .Must(address =>
                {
                    var existRequest = new DoesSameLibraryExistRequest
                    {
                        Address = address
                    };

                    return !requestClient.GetResponse<DoesSameLibraryExistResponse>(existRequest).Result.Message.IsExist;
                })
                .WithMessage("This address already exists");
        }
    }
}
