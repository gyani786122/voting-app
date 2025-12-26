using Voting.Application.Dtos;

namespace Voting.Application.UseCases.Voters;

/// <summary>
/// Interface for the use case of retrieving all voters.
/// </summary>
public interface IGetAllVotersUseCase
{
    /// <summary>
    /// Executes the use case to retrieve all voters.
    /// </summary>
    /// <returns>A list of voter DTOs.</returns>
    Task<List<VoterDto>> ExecuteAsync();
}
