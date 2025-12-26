namespace Voting.Domain;

/// <summary>
/// Represents a vote cast in the voting system.
/// Enforces one vote per voter via unique constraint on VoterId.
/// </summary>
public class Vote
{
    /// <summary>
    /// Unique identifier for the vote.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Foreign key referencing the voter who cast this vote.
    /// </summary>
    public Guid VoterId { get; set; }

    /// <summary>
    /// Foreign key referencing the candidate who received this vote.
    /// </summary>
    public Guid CandidateId { get; set; }

    /// <summary>
    /// UTC timestamp when the vote was cast.
    /// </summary>
    public DateTime CastAtUtc { get; set; }

    /// <summary>
    /// Navigation property for the voter.
    /// </summary>
    public Voter? Voter { get; set; }

    /// <summary>
    /// Navigation property for the candidate.
    /// </summary>
    public Candidate? Candidate { get; set; }
}
