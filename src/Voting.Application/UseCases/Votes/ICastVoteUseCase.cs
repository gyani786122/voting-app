using Voting.Application.Dtos;

namespace Voting.Application.UseCases.Votes;

/// <summary>
/// Interface for the use case of casting a vote.
/// </summary>
public interface ICastVoteUseCase
{
    /// <summary>
    /// Executes the use case to cast a vote.
    /// Enforces one vote per voter and updates vote counts.
    /// </summary>
    /// <param name="request">The cast vote request.</param>
    /// <returns>The cast vote response with updated voter and candidate information.</returns>
    /// <exception cref="EntityNotFoundException">Thrown if voter or candidate not found.</exception>
    /// <exception cref="VoterAlreadyVotedException">Thrown if voter has already cast a vote.</exception>
    Task<CastVoteResponse> ExecuteAsync(CastVoteRequest request);
}
