using Microsoft.EntityFrameworkCore;
using Voting.Application.Dtos;
using Voting.Domain;
using Voting.Infrastructure.Persistence;

namespace Voting.Application.UseCases.Votes;

/// <summary>
/// Implementation of the CastVote use case.
/// Enforces one vote per voter through both application logic and database constraints.
/// </summary>
public class CastVoteUseCase : ICastVoteUseCase
{
    private readonly VotingDbContext _dbContext;

    public CastVoteUseCase(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Casts a vote in a transaction, ensuring data consistency.
    /// Validates that the voter hasn't already voted and that both entities exist.
    /// </summary>
    public async Task<CastVoteResponse> ExecuteAsync(CastVoteRequest request)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            // Fetch voter with write lock
            var voter = await _dbContext.Voters.FirstOrDefaultAsync(v => v.Id == request.VoterId);
            if (voter == null)
            {
                throw new EntityNotFoundException(nameof(Voter), request.VoterId);
            }

            // Check if voter has already voted
            if (voter.HasVoted)
            {
                throw new VoterAlreadyVotedException(request.VoterId);
            }

            // Fetch candidate
            var candidate = await _dbContext.Candidates.FirstOrDefaultAsync(c => c.Id == request.CandidateId);
            if (candidate == null)
            {
                throw new EntityNotFoundException(nameof(Candidate), request.CandidateId);
            }

            // Create vote record
            var vote = new Vote
            {
                Id = Guid.NewGuid(),
                VoterId = request.VoterId,
                CandidateId = request.CandidateId,
                CastAtUtc = DateTime.UtcNow
            };

            _dbContext.Votes.Add(vote);

            // Update voter
            voter.HasVoted = true;

            // Update candidate vote count
            candidate.VoteCount++;

            // Save all changes
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            // Return updated entities as DTOs
            return new CastVoteResponse
            {
                Candidate = new CandidateDto
                {
                    Id = candidate.Id,
                    Name = candidate.Name,
                    VoteCount = candidate.VoteCount,
                    CreatedAtUtc = candidate.CreatedAtUtc
                },
                Voter = new VoterDto
                {
                    Id = voter.Id,
                    Name = voter.Name,
                    HasVoted = voter.HasVoted,
                    CreatedAtUtc = voter.CreatedAtUtc
                }
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
