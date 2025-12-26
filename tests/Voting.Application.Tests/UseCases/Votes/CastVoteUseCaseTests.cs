using Voting.Application.Dtos;
using Voting.Application.UseCases.Votes;
using Voting.Domain;
using Voting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Voting.Application.Tests.UseCases.Votes;

/// <summary>
/// Unit tests for the CastVote use case.
/// Verifies one-vote-per-voter enforcement and vote handling.
/// </summary>
public class CastVoteUseCaseTests
{
    /// <summary>
    /// Creates an in-memory database context for testing.
    /// </summary>
    private static VotingDbContext CreateTestDbContext(string? databaseName = null)
    {
        var options = new DbContextOptionsBuilder<VotingDbContext>()
            .UseInMemoryDatabase(databaseName: databaseName ?? Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        return new VotingDbContext(options);
    }

    [Fact]
    public async Task ExecuteAsync_WhenValidVote_ShouldCastVoteAndUpdateEntities()
    {
        // Arrange
        var dbContext = CreateTestDbContext();
        var voterId = Guid.NewGuid();
        var candidateId = Guid.NewGuid();

        var voter = new Voter { Id = voterId, Name = "Test Voter", HasVoted = false, CreatedAtUtc = DateTime.UtcNow };
        var candidate = new Candidate { Id = candidateId, Name = "Test Candidate", VoteCount = 0, CreatedAtUtc = DateTime.UtcNow };

        dbContext.Voters.Add(voter);
        dbContext.Candidates.Add(candidate);
        await dbContext.SaveChangesAsync();

        var useCase = new CastVoteUseCase(dbContext);
        var request = new CastVoteRequest { VoterId = voterId, CandidateId = candidateId };

        // Act
        var response = await useCase.ExecuteAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.True(response.Voter.HasVoted);
        Assert.Equal(1, response.Candidate.VoteCount);
        
        // Verify database state
        var updatedVoter = await dbContext.Voters.FindAsync(voterId);
        var updatedCandidate = await dbContext.Candidates.FindAsync(candidateId);
        
        Assert.NotNull(updatedVoter);
        Assert.True(updatedVoter.HasVoted);
        Assert.NotNull(updatedCandidate);
        Assert.Equal(1, updatedCandidate.VoteCount);
    }

    [Fact]
    public async Task ExecuteAsync_WhenVoterAlreadyVoted_ShouldThrowVoterAlreadyVotedException()
    {
        // Arrange
        var dbContext = CreateTestDbContext();
        var voterId = Guid.NewGuid();
        var candidateId1 = Guid.NewGuid();
        var candidateId2 = Guid.NewGuid();

        var voter = new Voter { Id = voterId, Name = "Test Voter", HasVoted = true, CreatedAtUtc = DateTime.UtcNow };
        var candidate1 = new Candidate { Id = candidateId1, Name = "Candidate 1", VoteCount = 1, CreatedAtUtc = DateTime.UtcNow };
        var candidate2 = new Candidate { Id = candidateId2, Name = "Candidate 2", VoteCount = 0, CreatedAtUtc = DateTime.UtcNow };

        dbContext.Voters.Add(voter);
        dbContext.Candidates.AddRange(candidate1, candidate2);
        await dbContext.SaveChangesAsync();

        var useCase = new CastVoteUseCase(dbContext);
        var request = new CastVoteRequest { VoterId = voterId, CandidateId = candidateId2 };

        // Act & Assert
        await Assert.ThrowsAsync<VoterAlreadyVotedException>(() => useCase.ExecuteAsync(request));
    }

    [Fact]
    public async Task ExecuteAsync_WhenVoterNotFound_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var dbContext = CreateTestDbContext();
        var candidateId = Guid.NewGuid();
        var nonExistentVoterId = Guid.NewGuid();

        var candidate = new Candidate { Id = candidateId, Name = "Test Candidate", VoteCount = 0, CreatedAtUtc = DateTime.UtcNow };

        dbContext.Candidates.Add(candidate);
        await dbContext.SaveChangesAsync();

        var useCase = new CastVoteUseCase(dbContext);
        var request = new CastVoteRequest { VoterId = nonExistentVoterId, CandidateId = candidateId };

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => useCase.ExecuteAsync(request));
    }

    [Fact]
    public async Task ExecuteAsync_WhenCandidateNotFound_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        var dbContext = CreateTestDbContext();
        var voterId = Guid.NewGuid();
        var nonExistentCandidateId = Guid.NewGuid();

        var voter = new Voter { Id = voterId, Name = "Test Voter", HasVoted = false, CreatedAtUtc = DateTime.UtcNow };

        dbContext.Voters.Add(voter);
        await dbContext.SaveChangesAsync();

        var useCase = new CastVoteUseCase(dbContext);
        var request = new CastVoteRequest { VoterId = voterId, CandidateId = nonExistentCandidateId };

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => useCase.ExecuteAsync(request));
    }

    [Fact]
    public async Task ExecuteAsync_WhenMultipleVotesAttempted_ShouldOnlyAllowFirstVote()
    {
        // Arrange
        var databaseName = Guid.NewGuid().ToString();
        var dbContext = CreateTestDbContext(databaseName);
        var voterId = Guid.NewGuid();
        var candidateId = Guid.NewGuid();

        var voter = new Voter { Id = voterId, Name = "Test Voter", HasVoted = false, CreatedAtUtc = DateTime.UtcNow };
        var candidate = new Candidate { Id = candidateId, Name = "Test Candidate", VoteCount = 0, CreatedAtUtc = DateTime.UtcNow };

        dbContext.Voters.Add(voter);
        dbContext.Candidates.Add(candidate);
        await dbContext.SaveChangesAsync();

        var useCase = new CastVoteUseCase(dbContext);
        var request = new CastVoteRequest { VoterId = voterId, CandidateId = candidateId };

        // Act
        var firstVote = await useCase.ExecuteAsync(request);
        Assert.NotNull(firstVote);
        
        // Try to cast second vote using a new context with the same database
        var dbContext2 = CreateTestDbContext(databaseName);
        var useCase2 = new CastVoteUseCase(dbContext2);

        // Assert
        await Assert.ThrowsAsync<VoterAlreadyVotedException>(() => useCase2.ExecuteAsync(request));
    }
}
