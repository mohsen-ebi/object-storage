using FluentValidation;
using Simple.Object.Storage.Application.Contracts.Requests;

namespace Simple.Object.Storage.Application.Contracts.Validators;

public class StoreTestRequestContractValidator : AbstractValidator<StoreTestRequestContract>
{
    public StoreTestRequestContractValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty()
            .GreaterThan(1)
            .WithMessage("Id must be greater that 1");
    }
}