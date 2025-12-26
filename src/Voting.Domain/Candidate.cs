namespace Voting.Domain;

/// <summary>
/// Represents a candidate in the voting system.
/// </summary>
public class Candidate
{
    /// <summary>
    /// Unique identifier for the candidate.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the candidate.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Total number of votes received.
    /// </summary>
    public int VoteCount { get; set; }

    /// <summary>
    /// UTC timestamp when the candidate was created.
    /// </summary>
    public DateTime CreatedAtUtc { get; set; }

    /// <summary>
    /// Navigation property for votes cast for this candidate.
    /// </summary>
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
