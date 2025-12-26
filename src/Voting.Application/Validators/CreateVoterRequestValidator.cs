using FluentValidation;
using Voting.Application.Dtos;

namespace Voting.Application.Validators;

/// <summary>
/// Validator for CreateVoterRequest.
/// </summary>
public class CreateVoterRequestValidator : AbstractValidator<CreateVoterRequest>
{
    public CreateVoterRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Voter name is required.")
            .MaximumLength(255).WithMessage("Voter name must not exceed 255 characters.");
    }
}
