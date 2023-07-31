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

    [HttpPost]
    public async Task<IResponse> InsertGuarantorAsync([FromBody] CreateGuarantorViewModel ViewModel)
    {
        return await GuarantorService.InsertGuarantorAsync(ViewModel);
    }

    [HttpGet("/Api/Guarantors")]
    public async Task<IResponse> FetchGuarantorsAsync()
    {
        return await GuarantorService.FetchGuarantorsAsync().ToResponseAsync();
    }
}
