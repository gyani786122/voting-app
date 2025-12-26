namespace Voting.Application.Dtos;

/// <summary>
/// Request DTO for creating a new voter.
/// </summary>
public class CreateVoterRequest
{
    /// <summary>
    /// Name of the voter to be created.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
