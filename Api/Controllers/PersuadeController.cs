using Microsoft.AspNetCore.Mvc;
using PersuadeMate.Api.Requests;
using PersuadeMate.Api.Responses;
using PersuadeMate.Data;
using PersuadeMate.Data.Interfaces;

namespace PersuadeMate.Api.Controllers;

/// <summary>
/// ひとこと提言を取得するためのAPIコントローラです
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PersuadeController(IAdvisor advisor, IAgesRepository agesRepository) : ControllerBase
{
    /// <summary>
    /// ひとこと提言を取得します
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ProposalResponse>> GetProposal([FromBody] SuggestionRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var age = request.ProposedTo.Age is null ? null : agesRepository.GetAgeByKey(request.ProposedTo.Age);

        var question = new Question()
        {
            Persona = new Persona()
            {
                Gender = request.ProposedTo.Gender,
                Age = age,
                Preferences = request.ProposedTo.Preferences,
            },
            Suggestion = request.Suggestion,
        };

        var response = await advisor.GetAdviceAsync(question);
        if (response.IsError(out var errorMessage))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
        }

        response.IsOk(out var candidates);

        return Ok(new ProposalResponse { Proposals = candidates.ToList() });
    }
}
