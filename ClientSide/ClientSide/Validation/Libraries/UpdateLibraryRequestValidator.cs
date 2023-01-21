using FluentValidation;
using Broker.Requests;
using Broker.Responses;
using MassTransit;
using FluentValidation.Results;

namespace ClientSide.Validation.Libraries
{
    public interface IUpdateLibraryRequestValidator
    {
        public ValidationResult Validate(UpdateLibraryRequest request);
    }

    public class UpdateLibraryRequestValidator : AbstractValidator<UpdateLibraryRequest>, IUpdateLibraryRequestValidator
    {
        public UpdateLibraryRequestValidator(IRequestClient<DoesSameLibraryExistRequest> requestClient)
        {
            RuleFor(request => request.OldAddress)
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

            RuleFor(request => request.NewAddress)
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
