
using Microsoft.AspNetCore.Mvc;
using PersuadeMate.Api.Requests;
using PersuadeMate.Api.Responses;

namespace PersuadeMate.Api.Controllers;

/// <summary>
/// ひとこと提言を取得するためのAPIコントローラです
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PersuadeController : ControllerBase
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

        var response = new ProposalResponse()
        {
            Proposals = ["お肌の健康には、温泉でゆっくりするのが一番"]
        };

        return Ok(response);
    }
}
