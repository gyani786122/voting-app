using Voting.Domain;
using Voting.Infrastructure.Persistence;

namespace Voting.Api;

/// <summary>
/// Database seeding utility to populate sample data.
/// </summary>
public static class DatabaseSeeder
{
    /// <summary>
    /// Seeds the database with sample candidates and voters.
    /// </summary>
    public static async Task SeedAsync(VotingDbContext dbContext)
    {
        // Create database if it doesn't exist
        await dbContext.Database.EnsureCreatedAsync();

        // Skip if data already exists
        if (dbContext.Candidates.Any() || dbContext.Voters.Any())
        {
            return;
        }

        var candidates = new List<Candidate>
        {
            new Candidate
            {
                Id = Guid.NewGuid(),
                Name = "Alice Johnson",
                VoteCount = 0,
                CreatedAtUtc = DateTime.UtcNow
            },
            new Candidate
            {
                Id = Guid.NewGuid(),
                Name = "Bob Smith",
                VoteCount = 0,
                CreatedAtUtc = DateTime.UtcNow
            },
            new Candidate
            {
                Id = Guid.NewGuid(),
                Name = "Carol White",
                VoteCount = 0,
                CreatedAtUtc = DateTime.UtcNow
            },
            new Candidate
            {
                Id = Guid.NewGuid(),
                Name = "David Brown",
                VoteCount = 0,
                CreatedAtUtc = DateTime.UtcNow
            }
        };

        var voters = new List<Voter>
        {
            new Voter
            {
                Id = Guid.NewGuid(),
                Name = "Emma Davis",
                HasVoted = false,
                CreatedAtUtc = DateTime.UtcNow
            },
            new Voter
            {
                Id = Guid.NewGuid(),
                Name = "Frank Miller",
                HasVoted = false,
                CreatedAtUtc = DateTime.UtcNow
            },
            new Voter
            {
                Id = Guid.NewGuid(),
                Name = "Grace Lee",
                HasVoted = false,
                CreatedAtUtc = DateTime.UtcNow
            },
            new Voter
            {
                Id = Guid.NewGuid(),
                Name = "Henry Taylor",
                HasVoted = false,
                CreatedAtUtc = DateTime.UtcNow
            },
            new Voter
            {
                Id = Guid.NewGuid(),
                Name = "Iris Martinez",
                HasVoted = false,
                CreatedAtUtc = DateTime.UtcNow
            }
        };

        dbContext.Candidates.AddRange(candidates);
        dbContext.Voters.AddRange(voters);

        await dbContext.SaveChangesAsync();
    }
}
