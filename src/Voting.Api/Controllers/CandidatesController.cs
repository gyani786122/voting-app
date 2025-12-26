using FluentValidation;
using Voting.Application.Dtos;
using Voting.Application.UseCases.Candidates;
using Microsoft.AspNetCore.Mvc;

namespace Voting.Api.Controllers;

/// <summary>
/// API controller for candidate-related operations.
/// </summary>
[ApiController]
[Route("api/candidates")]
public class CandidatesController : ControllerBase
{
    private readonly IGetAllCandidatesUseCase _getAllCandidatesUseCase;
    private readonly ICreateCandidateUseCase _createCandidateUseCase;
    private readonly IValidator<CreateCandidateRequest> _createCandidateValidator;

    public CandidatesController(
        IGetAllCandidatesUseCase getAllCandidatesUseCase,
        ICreateCandidateUseCase createCandidateUseCase,
        IValidator<CreateCandidateRequest> createCandidateValidator)
    {
        _getAllCandidatesUseCase = getAllCandidatesUseCase;
        _createCandidateUseCase = createCandidateUseCase;
        _createCandidateValidator = createCandidateValidator;
    }

    /// <summary>
    /// Retrieves all candidates.
    /// </summary>
    /// <returns>List of candidates sorted by vote count (descending).</returns>
    [HttpGet]
    public async Task<ActionResult<List<CandidateDto>>> GetAllCandidates()
    {
        var candidates = await _getAllCandidatesUseCase.ExecuteAsync();
        return Ok(candidates);
    }

    /// <summary>
    /// Creates a new candidate.
    /// </summary>
    /// <param name="request">The candidate creation request.</param>
    /// <returns>The created candidate.</returns>
    [HttpPost]
    public async Task<ActionResult<CandidateDto>> CreateCandidate([FromBody] CreateCandidateRequest request)
    {
        var validationResult = await _createCandidateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new { errors = validationResult.Errors.Select(e => e.ErrorMessage) });
        }

        var candidate = await _createCandidateUseCase.ExecuteAsync(request);
        return CreatedAtAction(nameof(GetAllCandidates), new { id = candidate.Id }, candidate);
    }
}
