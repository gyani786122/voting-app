namespace Voting.Application.Dtos;

/// <summary>
/// Data Transfer Object for Candidate.
/// </summary>
public class CandidateDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Candidate name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Number of votes received.
    /// </summary>
    public int VoteCount { get; set; }

    /// <summary>
    /// Creation timestamp in UTC.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }
}
