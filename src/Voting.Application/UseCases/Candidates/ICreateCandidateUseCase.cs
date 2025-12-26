using Voting.Application.Dtos;

namespace Voting.Application.UseCases.Candidates;

/// <summary>
/// Interface for the use case of creating a new candidate.
/// </summary>
public interface ICreateCandidateUseCase
{
    /// <summary>
    /// Executes the use case to create a new candidate.
    /// </summary>
    /// <param name="request">The create candidate request.</param>
    /// <returns>The created candidate DTO.</returns>
    Task<CandidateDto> ExecuteAsync(CreateCandidateRequest request);
}
