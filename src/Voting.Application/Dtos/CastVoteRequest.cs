namespace Voting.Application.Dtos;

/// <summary>
/// Request DTO for casting a vote.
/// </summary>
public class CastVoteRequest
{
    /// <summary>
    /// Identifier of the voter casting the vote.
    /// </summary>
    public Guid VoterId { get; set; }

    /// <summary>
    /// Identifier of the candidate being voted for.
    /// </summary>
    public Guid CandidateId { get; set; }
}
