using Microsoft.EntityFrameworkCore;
using Voting.Domain;

namespace Voting.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core database context for the voting application.
/// Configures the database schema and relationships.
/// </summary>
public class VotingDbContext : DbContext
{
    public VotingDbContext(DbContextOptions<VotingDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// DbSet for candidates.
    /// </summary>
    public DbSet<Candidate> Candidates { get; set; } = null!;

    /// <summary>
    /// DbSet for voters.
    /// </summary>
    public DbSet<Voter> Voters { get; set; } = null!;

    /// <summary>
    /// DbSet for votes.
    /// </summary>
    public DbSet<Vote> Votes { get; set; } = null!;

    /// <summary>
    /// Configures the database schema and constraints.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Candidate
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.VoteCount).HasDefaultValue(0);
            entity.Property(e => e.CreatedAtUtc).IsRequired();
            
            entity.HasMany(e => e.Votes)
                .WithOne(v => v.Candidate)
                .HasForeignKey(v => v.CandidateId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Voter
        modelBuilder.Entity<Voter>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
            entity.Property(e => e.HasVoted).HasDefaultValue(false);
            entity.Property(e => e.CreatedAtUtc).IsRequired();
            
            entity.HasOne(e => e.Vote)
                .WithOne(v => v.Voter)
                .HasForeignKey<Vote>(v => v.VoterId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Vote
        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.VoterId).IsRequired();
            entity.Property(e => e.CandidateId).IsRequired();
            entity.Property(e => e.CastAtUtc).IsRequired();
            
            // Unique constraint: one vote per voter
            entity.HasIndex(e => e.VoterId).IsUnique();
            
            entity.HasOne(e => e.Voter)
                .WithOne(v => v.Vote)
                .HasForeignKey<Vote>(v => v.VoterId);

            entity.HasOne(e => e.Candidate)
                .WithMany(c => c.Votes)
                .HasForeignKey(e => e.CandidateId);
        });
    }
}
