using FluentValidation;
using Voting.Application.Dtos;
using Voting.Application.UseCases.Votes;
using Voting.Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Voting.Api.Controllers;

/// <summary>
/// API controller for vote-related operations.
/// </summary>
[ApiController]
[Route("api/votes")]
public class VotesController : ControllerBase
{
    private readonly ICastVoteUseCase _castVoteUseCase;
    private readonly IValidator<CastVoteRequest> _castVoteValidator;
    private readonly IHubContext<VotingHub> _votingHubContext;

    public VotesController(
        ICastVoteUseCase castVoteUseCase,
        IValidator<CastVoteRequest> castVoteValidator,
        IHubContext<VotingHub> votingHubContext)
    {
        _castVoteUseCase = castVoteUseCase;
        _castVoteValidator = castVoteValidator;
        _votingHubContext = votingHubContext;
    }

    /// <summary>
    /// Casts a vote for the specified voter and candidate.
    /// Returns updated voter and candidate information.
    /// </summary>
    /// <param name="request">The vote casting request.</param>
    /// <returns>Updated voter and candidate DTOs.</returns>
    [HttpPost]
    public async Task<ActionResult<CastVoteResponse>> CastVote([FromBody] CastVoteRequest request)
    {
        var validationResult = await _castVoteValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
        }

        try
        {
            var response = await _castVoteUseCase.ExecuteAsync(request);

            // Broadcast vote cast event to all connected clients via SignalR
            await _votingHubContext.Clients.All.SendAsync("VoteCast", response);

            return Ok(response);
        }
        catch (VoterAlreadyVotedException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
