﻿namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;

using SysCredit.Helpers;
using SysCredit.Models;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("Api/[Controller]")]
public class PaymentFrequencyController(IPaymentFrequencyService PaymentFrequencyService, ILogger<PaymentFrequencyController> Logger) : ControllerBase
{
    /// <summary>
    /// Este método es un controlador HTTP GET que responde a la ruta "/Api/PaymentFrequency"
    /// Convierte el resultado en una respuesta HTTP, con códigos de estado 200 OK y 500 Internal Server Error definidos.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchPaymentFrequencyAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency");
        return await PaymentFrequencyService.FetchPaymentFrequencyAsync().ToResponseAsync();
    }

    /// <summary>
    /// Este método es un controlador HTTP GET que responde a la ruta "/Api/PaymentFrequency/Complete"
    /// Convierte el resultado en una respuesta HTTP, con códigos de estado 200 OK y 500 Internal Server Error definidos.
    /// </summary>
    /// <returns></returns>
    [HttpGet("Complete")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchPaymentFrequencyCompleteAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/PaymentFrequency/Complete");
        return await PaymentFrequencyService.FetchPaymentFrequencyCompleteAsync().ToResponseAsync();
    }
}
