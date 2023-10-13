using FluentValidation;
using Simple.Object.Storage.Application.Contracts.Requests;

namespace Simple.Object.Storage.Application.Contracts.Validators;

public class StoreRequestContractValidator : AbstractValidator<StoreRequestContract>
{
    public StoreRequestContractValidator()
    {
        RuleFor(c => c.File)
            .NotEmpty()
            .WithMessage(c => $"{c.File} cannot be empty");
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(c => $"{nameof(c.Name)} cannot be empty");
    }
}