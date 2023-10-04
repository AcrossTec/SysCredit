namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Interfaces.Services;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

[ApiController]
[Route("Api/[Controller]")]
public class PaymentPlanController(IPaymentPlanService PaymentPlanService, ILogger<PaymentPlanController> Logger) : ControllerBase
{
    [HttpGet("{LoanId}")]
    [ProducesResponseType(typeof(IResponse<PaymentPlanInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<PaymentPlanInfo>>> FetchPaymentPlanByLoanId([FromRoute] long LoanId)
    {
        var Result = await PaymentPlanService.FetchPaymentPlanByLoanId(LoanId);

        return StatusCode(StatusCodes.Status200OK, Result);
    }
}