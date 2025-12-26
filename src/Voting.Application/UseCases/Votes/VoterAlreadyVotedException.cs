using Voting.Application.Dtos;

namespace Voting.Application.UseCases.Votes;

/// <summary>
/// Custom exception thrown when attempting to cast a duplicate vote.
/// </summary>
public class VoterAlreadyVotedException : Exception
{
    public VoterAlreadyVotedException(Guid voterId)
        : base($"Voter with ID '{voterId}' has already cast their vote.")
    {
    }
}
