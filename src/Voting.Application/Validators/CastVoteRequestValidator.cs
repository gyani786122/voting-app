using FluentValidation;
using Voting.Application.Dtos;

namespace Voting.Application.Validators;

/// <summary>
/// Validator for CastVoteRequest.
/// </summary>
public class CastVoteRequestValidator : AbstractValidator<CastVoteRequest>
{
    public CastVoteRequestValidator()
    {
        RuleFor(x => x.VoterId)
            .NotEmpty().WithMessage("Voter ID is required.");

        RuleFor(x => x.CandidateId)
            .NotEmpty().WithMessage("Candidate ID is required.");
    }
}
