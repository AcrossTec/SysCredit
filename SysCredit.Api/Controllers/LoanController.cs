namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Loan;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

/// <summary>
///     Controlador API para manejar solicitudes relacionadas con préstamos.
/// </summary>
/// <param name="LoanService">El servicio de préstamos para procesar operaciones relacionadas con préstamos.</param>
/// <param name="Logger">El registro para almacenar información.</param>
[ApiController]
[Route("Api/[Controller]")]
public class LoanController(ILoanService LoanService, ILogger<LoanController> Logger) : ControllerBase
{
    /// <summary>
    ///     Controlador para obtener la forma de pago por id y por id de prestamo
    /// </summary>
    /// <param name="Request">Recibe el id de la forma de pago y el id del prestamo</param>
    /// <returns></returns>
    [HttpGet("{LoanId}/PaymentPlan/{PaymentPlanId}")]
    [ProducesResponseType(typeof(IResponse<PaymentPlanInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<PaymentPlanInfo?>> FetchFromLoanThePaymentPlanByIdAndLoanId([FromRoute] LoandIdWithPaymentPlanIdRequest Request)
    {
        return await LoanService.FetchFromLoanThePaymentPlanByIdAndLoanId(Request).ToResponseAsync();
    }
}