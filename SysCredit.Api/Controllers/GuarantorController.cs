namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Guarantors;

using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
/// <param name="GuarantorService"></param>
[ApiController]
[Route("Api/[Controller]")]
public class GuarantorController(IGuarantorService GuarantorService, ILogger<GuarantorController> Logger) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreateGuarantorRequest>))]
    public async Task<IActionResult> InsertGuarantorAsync([FromBody] CreateGuarantorRequest Request)
    {
        Logger.LogInformation("EndPoint[POST]: /Api/Guarantor");

        var Result = await GuarantorService.InsertGuarantorAsync(Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpGet("/Api/Guarantors")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<FetchGuarantor>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchGuarantorsAsync([FromQuery] PaginationRequest Request)
    {
        if (Request.Offset.HasValue && Request.Limit.HasValue)
        {
            return await GuarantorService.FetchGuarantorsAsync(Request).ToResponseAsync();
        }
        else
        {
            return await GuarantorService.FetchGuarantorsAsync().ToResponseAsync();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpGet("/Api/Guarantor/Search")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<FetchGuarantor>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> SearchGuarantorAsync([FromQuery] SearchRequest Request)
    {
        return await GuarantorService.SearchGuarantorAsync(Request).ToResponseAsync();
    }
}
