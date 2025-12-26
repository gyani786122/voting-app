using Voting.Application.Dtos;

namespace Voting.Application.UseCases.Voters;

/// <summary>
/// Interface for the use case of creating a new voter.
/// </summary>
public interface ICreateVoterUseCase
{
    /// <summary>
    /// Executes the use case to create a new voter.
    /// </summary>
    /// <param name="request">The create voter request.</param>
    /// <returns>The created voter DTO.</returns>
    Task<VoterDto> ExecuteAsync(CreateVoterRequest request);
}
