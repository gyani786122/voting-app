namespace Voting.Application.Dtos;

/// <summary>
/// Response DTO for a vote cast operation.
/// Contains updated voter and candidate information.
/// </summary>
public class CastVoteResponse
{
    /// <summary>
    /// Updated candidate information.
    /// </summary>
    public CandidateDto Candidate { get; set; } = null!;

    /// <summary>
    /// Updated voter information.
    /// </summary>
    public VoterDto Voter { get; set; } = null!;
}
