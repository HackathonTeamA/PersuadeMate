using Microsoft.AspNetCore.Mvc;
using PersuadeMate.Api.Requests;
using PersuadeMate.Api.Responses;
using PersuadeMate.Data.Interfaces;

namespace PersuadeMate.Api.Controllers;

/// <summary>
/// ひとこと提言を取得するためのAPIコントローラです
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PersuadeController(IAdvisor advisor) : ControllerBase
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

        var response = await advisor.GetAdviceAsync(request.Interviews, request.Suggestion);
        if (response.IsError(out var errorMessage))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
        }

        response.IsOk(out var candidates);

        return Ok(new ProposalResponse { Proposals = candidates.ToList() });
    }
}
