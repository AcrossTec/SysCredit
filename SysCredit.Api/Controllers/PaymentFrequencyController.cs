namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;

using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("Api/[Controller]")]
public class PaymentFrequencyController(IPaymentFrequencyService PaymentFrequencyService) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IResponse> FetchPaymentFrequencyAsync()
    {
        return await PaymentFrequencyService.FetchPaymentFrequencyAsync().ToResponseAsync();
    }
}
