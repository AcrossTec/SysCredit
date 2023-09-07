namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;

using SysCredit.Helpers;

[ApiController]
[Route("Api/[controller]")]
public class PaymentFrequencyController : ControllerBase
{
    private readonly IPaymentFrequencyService PaymentFrequencyService;

    public PaymentFrequencyController(IPaymentFrequencyService PaymentFrequencyService)
    {
        this.PaymentFrequencyService = PaymentFrequencyService;
    }

    [HttpGet]
    public async Task<IResponse> FetchPaymentFrequencyAsync()
    {
        return await PaymentFrequencyService.FetchPaymentFrequencyAsync().ToResponseAsync();
    }
}
