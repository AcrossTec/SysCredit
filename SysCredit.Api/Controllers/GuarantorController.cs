namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.DataTransferObject.StoredProcedures;
using SysCredit.Api.Extensions;
using SysCredit.Api.Helpers;
using SysCredit.Api.Interfaces;
using SysCredit.Api.ViewModels.Guarantors;

[ApiController]
[Route("Api/[Controller]")]
public class GuarantorController : ControllerBase
{
    private readonly IGuarantorService GuarantorService;

    public GuarantorController(IGuarantorService GuarantorService)
    {
        this.GuarantorService = GuarantorService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ViewModel"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreateGuarantorRequest>))]
    public async Task<IActionResult> InsertGuarantorAsync([FromBody] CreateGuarantorRequest ViewModel)
    {
        var Result = await GuarantorService.InsertGuarantorAsync(ViewModel);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(ViewModel));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("/Api/Guarantors")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<FetchGuarantor>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchGuarantorsAsync()
    {
        return await GuarantorService.FetchGuarantorsAsync().ToResponseAsync();
    }
}
