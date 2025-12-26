using Voting.Application.Dtos;
using Voting.Domain;
using Voting.Infrastructure.Persistence;

namespace Voting.Application.UseCases.Candidates;

/// <summary>
/// Implementation of the CreateCandidate use case.
/// </summary>
public class CreateCandidateUseCase : ICreateCandidateUseCase
{
    private readonly VotingDbContext _dbContext;

    public CreateCandidateUseCase(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Creates a new candidate and saves it to the database.
    /// </summary>
    public async Task<CandidateDto> ExecuteAsync(CreateCandidateRequest request)
    {
        var candidate = new Candidate
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            VoteCount = 0,
            CreatedAtUtc = DateTime.UtcNow
        };

        _dbContext.Candidates.Add(candidate);
        await _dbContext.SaveChangesAsync();

        return new CandidateDto
        {
            Id = candidate.Id,
            Name = candidate.Name,
            VoteCount = candidate.VoteCount,
            CreatedAtUtc = candidate.CreatedAtUtc
        };
    }
}
