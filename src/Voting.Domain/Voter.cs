namespace Voting.Domain;

/// <summary>
/// Represents a voter in the voting system.
/// </summary>
public class Voter
{
    /// <summary>
    /// Unique identifier for the voter.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the voter.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the voter has already cast their vote.
    /// </summary>
    public bool HasVoted { get; set; }

    /// <summary>
    /// UTC timestamp when the voter was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }

    /// <summary>
    /// Navigation property for the vote cast by this voter.
    /// </summary>
    public Vote? Vote { get; set; }
}
