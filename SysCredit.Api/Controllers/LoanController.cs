namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Interfaces.Services;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

[ApiController]
[Route("Api/[Controller]")]
public class LoanController(ILoanService LoanService, ILogger<LoanController> Logger) : ControllerBase
{
    [HttpGet("{LoanId}")]
    [ProducesResponseType(typeof(IResponse<LoanInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchLoanByLoanIdAsync([FromRoute] long? LoanId)
    {
        return await LoanService.FetchLoanByLoanIdAsync(LoanId);
    }
}
