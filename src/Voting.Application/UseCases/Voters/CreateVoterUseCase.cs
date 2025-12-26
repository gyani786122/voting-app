using Voting.Application.Dtos;
using Voting.Domain;
using Voting.Infrastructure.Persistence;

namespace Voting.Application.UseCases.Voters;

/// <summary>
/// Implementation of the CreateVoter use case.
/// </summary>
public class CreateVoterUseCase : ICreateVoterUseCase
{
    private readonly VotingDbContext _dbContext;

    public CreateVoterUseCase(VotingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Creates a new voter and saves it to the database.
    /// </summary>
    public async Task<VoterDto> ExecuteAsync(CreateVoterRequest request)
    {
        var voter = new Voter
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            HasVoted = false,
            CreatedAtUtc = DateTime.UtcNow
        };

        _dbContext.Voters.Add(voter);
        await _dbContext.SaveChangesAsync();

        return new VoterDto
        {
            Id = voter.Id,
            Name = voter.Name,
            HasVoted = voter.HasVoted,
            CreatedAtUtc = voter.CreatedAtUtc
        };
    }
}
