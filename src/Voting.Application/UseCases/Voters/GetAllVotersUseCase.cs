using Microsoft.EntityFrameworkCore;
using Voting.Application.Dtos;
using Voting.Infrastructure.Persistence;

namespace Voting.Application.UseCases.Voters;

/// <summary>
/// Implementation of the GetAllVoters use case.
/// </summary>
public class GetAllVotersUseCase : IGetAllVotersUseCase
{
    private readonly VotingDbContext _dbContext;

    public GetAllVotersUseCase(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Retrieves all voters from the database.
    /// </summary>
    public async Task<List<VoterDto>> ExecuteAsync()
    {
        var voters = await _dbContext.Voters
            .OrderBy(v => v.Name)
            .Select(v => new VoterDto
            {
                Id = v.Id,
                Name = v.Name,
                HasVoted = v.HasVoted,
                CreatedAtUtc = v.CreatedAtUtc
            })
            .ToListAsync();

        return voters;
    }
}
