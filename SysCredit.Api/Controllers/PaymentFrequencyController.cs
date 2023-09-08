namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
/// <param name="PaymentFrequencyService"></param>
/// <param name="Logger"></param>
[ApiController]
[Route("Api/[Controller]")]
public class PaymentFrequencyController(IPaymentFrequencyService PaymentFrequencyService, ILogger<PaymentFrequencyController> Logger) : ControllerBase
{
    [HttpGet]
    public async Task<IResponse> FetchPaymentFrequencyAsync()
    {
        return await PaymentFrequencyService.FetchPaymentFrequencyAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PaymentFrequencyId"></param>
    /// <returns></returns>
    [HttpGet("{PaymentFrequencyId}")]
    [ProducesResponseType(typeof(IResponse<PaymentFrequencyInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId)
    {
        return await PaymentFrequencyService.FetchPaymentFrequencyByIdAsync(PaymentFrequencyId)!.ToResponseAsync();
    }
}
