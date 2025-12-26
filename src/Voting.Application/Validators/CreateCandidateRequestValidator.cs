using FluentValidation;
using Voting.Application.Dtos;

namespace Voting.Application.Validators;

/// <summary>
/// Validator for CreateCandidateRequest.
/// </summary>
public class CreateCandidateRequestValidator : AbstractValidator<CreateCandidateRequest>
{
    public CreateCandidateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Candidate name is required.")
            .MaximumLength(255).WithMessage("Candidate name must not exceed 255 characters.");
    }
}
