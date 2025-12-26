namespace Voting.Application.Dtos;

/// <summary>
/// Request DTO for creating a new candidate.
/// </summary>
public class CreateCandidateRequest
{
    /// <summary>
    /// Name of the candidate to be created.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
