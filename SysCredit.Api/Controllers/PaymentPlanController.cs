namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.PaymentPlans;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

[ApiController]
[Route("Api/[Controller]")]
public class PaymentPlanController(IPaymentPlanService PaymentPlanService, ILogger<PaymentPlanController> Logger) : ControllerBase
{
    [HttpGet("{PaymentPlanId}/Details")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<PaymentPlanDetailsInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<PaymentPlanDetailsInfo>>>> FetchPaymentPlanDetailsByPaymentPlanIdAsync([FromRoute] PaymentPlanIdRequest Request)
    {
        var Result = await PaymentPlanService.FetchPaymentPlanDetailsByPaymentPlanIdAsync(Request);

        return StatusCode(StatusCodes.Status200OK, Result);
    }
}
