using Voting.Application.Dtos;

namespace Voting.Application.UseCases.Votes;

/// <summary>
/// Custom exception thrown when a required entity is not found.
/// </summary>
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityType, Guid id)
        : base($"{entityType} with ID '{id}' was not found.")
    {
    }
}
