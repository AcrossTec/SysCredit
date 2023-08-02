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
    [ProducesErrorResponseType(typeof(IResponse<CreateGuarantorViewModel>))]
    public async Task<IActionResult> InsertGuarantorAsync([FromBody] CreateGuarantorViewModel ViewModel)
    {
        return await GuarantorService.InsertGuarantorAsync(ViewModel).ToActionResultAsync(StatusCodes.Status201Created, ViewModel);
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
