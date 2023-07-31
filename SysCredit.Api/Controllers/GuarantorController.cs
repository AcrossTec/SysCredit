namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Extensions;
using SysCredit.Api.Helpers;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Services;
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
    [ProducesErrorResponseType(typeof(IResponse<CreateGuarantorViewModel>))]
    public async Task<IActionResult> InsertGuarantorAsync([FromBody] CreateGuarantorViewModel ViewModel)
    {
        var ServiceResult = await GuarantorService.InsertGuarantorAsync(ViewModel);

        if (ServiceResult.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await ServiceResult.ToResponseWithReplaceDataAsync(ViewModel));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, ServiceResult);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("/Api/Guarantors")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<GuarantorDataTransferObject>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchGuarantorsAsync()
    {
        return await GuarantorService.FetchGuarantorsAsync().ToResponseAsync();
    }
}
