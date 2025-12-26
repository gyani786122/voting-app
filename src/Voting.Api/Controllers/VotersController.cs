using FluentValidation;
using Voting.Application.Dtos;
using Voting.Application.UseCases.Voters;
using Microsoft.AspNetCore.Mvc;

namespace Voting.Api.Controllers;

/// <summary>
/// API controller for voter-related operations.
/// </summary>
[ApiController]
[Route("api/voters")]
public class VotersController : ControllerBase
{
    private readonly IGetAllVotersUseCase _getAllVotersUseCase;
    private readonly ICreateVoterUseCase _createVoterUseCase;
    private readonly IValidator<CreateVoterRequest> _createVoterValidator;

    public VotersController(
        IGetAllVotersUseCase getAllVotersUseCase,
        ICreateVoterUseCase createVoterUseCase,
        IValidator<CreateVoterRequest> createVoterValidator)
    {
        _getAllVotersUseCase = getAllVotersUseCase;
        _createVoterUseCase = createVoterUseCase;
        _createVoterValidator = createVoterValidator;
    }

    /// <summary>
    /// Retrieves all voters.
    /// </summary>
    /// <returns>List of voters sorted by name.</returns>
    [HttpGet]
    public async Task<ActionResult<List<VoterDto>>> GetAllVoters()
    {
        var voters = await _getAllVotersUseCase.ExecuteAsync();
        return Ok(voters);
    }

    /// <summary>
    /// Creates a new voter.
    /// </summary>
    /// <param name="request">The voter creation request.</param>
    /// <returns>The created voter.</returns>
    [HttpPost]
    public async Task<ActionResult<VoterDto>> CreateVoter([FromBody] CreateVoterRequest request)
    {
        var validationResult = await _createVoterValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
        }

        var voter = await _createVoterUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(GetAllVoters), new { id = voter.Id }, voter);
    }
}
