using Microsoft.AspNetCore.SignalR;

namespace Voting.Api.Hubs;

/// <summary>
/// SignalR Hub for real-time voting updates.
/// Clients subscribe to receive updates when votes are cast.
/// </summary>
public class VotingHub : Hub
{
    /// <summary>
    /// Called when a vote is cast to broadcast updated candidate and voter info.
    /// </summary>
    /// <param name="candidateId">ID of the candidate receiving the vote.</param>
    /// <param name="candidateName">Name of the candidate.</param>
    /// <param name="newVoteCount">Updated vote count.</param>
    /// <param name="voterId">ID of the voter.</param>
    /// <param name="voterName">Name of the voter.</param>
    /// <param name="hasVoted">Whether the voter has now voted.</param>
    public async Task BroadcastVoteCast(
        Guid candidateId, string candidateName, int newVoteCount,
        Guid voterId, string voterName, bool hasVoted)
    {
        await Clients.All.SendAsync("VoteCast",
            new
            {
                Candidate = new { Id = candidateId, Name = candidateName, VoteCount = newVoteCount },
                Voter = new { Id = voterId, Name = voterName, HasVoted = hasVoted }
            });
    }
}
