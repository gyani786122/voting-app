using Voting.Application.Dtos;

namespace Voting.Application.UseCases.Candidates;

/// <summary>
/// Interface for the use case of retrieving all candidates.
/// </summary>
public interface IGetAllCandidatesUseCase
{
    /// <summary>
    /// Executes the use case to retrieve all candidates.
    /// </summary>
    /// <returns>A list of candidate DTOs.</returns>
    Task<List<CandidateDto>> ExecuteAsync();
}
