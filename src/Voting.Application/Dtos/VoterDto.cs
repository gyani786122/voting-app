namespace Voting.Application.Dtos;

/// <summary>
/// Data Transfer Object for Voter.
/// </summary>
public class VoterDto
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Voter name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Whether the voter has already voted.
    /// </summary>
    public bool HasVoted { get; set; }

    /// <summary>
    /// Creation timestamp in UTC.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }
}
