namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;

using SysCredit.Helpers;

[ApiController]
[Route("Api/[controller]")]
public class LoanTypeController : ControllerBase
{
    private readonly ILoanTypeService LoanTypeService;

    public LoanTypeController(ILoanTypeService LoanTypeService)
    {
        this.LoanTypeService = LoanTypeService;
    }

    [HttpGet]
    public async Task<IResponse> FetchLoanTypeAsync()
    {
        return await LoanTypeService.FetchLoanTypeAsync().ToResponseAsync();
    }

    [HttpGet("Complete")]
    public async Task<IResponse> FetchLoanTypeComplete()
    {
        return await LoanTypeService.FetchLoanTypeCompleteAsync().ToResponseAsync();
    }
}
