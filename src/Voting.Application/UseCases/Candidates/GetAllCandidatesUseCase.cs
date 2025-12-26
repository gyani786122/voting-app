using Microsoft.EntityFrameworkCore;
using Voting.Application.Dtos;
using Voting.Infrastructure.Persistence;

namespace Voting.Application.UseCases.Candidates;

/// <summary>
/// Implementation of the GetAllCandidates use case.
/// </summary>
public class GetAllCandidatesUseCase : IGetAllCandidatesUseCase
{
    private readonly VotingDbContext _dbContext;

    public GetAllCandidatesUseCase(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves all candidates from the database.
    /// </summary>
    public async Task<List<CandidateDto>> ExecuteAsync()
    {
        var candidates = await _dbContext.Candidates
            .OrderByDescending(c => c.VoteCount)
            .ThenBy(c => c.Name)
            .Select(c => new CandidateDto
            {
                Id = c.Id,
                Name = c.Name,
                VoteCount = c.VoteCount,
                CreatedAtUtc = c.CreatedAtUtc
            })
            .ToListAsync();

        return candidates;
    }
}
