namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Loans;
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
    ///     Obtiene los detalles del plan de pagos para un ID de préstamo dado.
    /// </summary>
    /// <param name="Request">La solicitud de ID de préstamo.</param>
    /// <returns>Un ActionResult que contiene información del plan de pagos.</returns>
    [HttpGet("{LoanId}/PaymentPlan")]
    [ProducesResponseType(typeof(IResponse<PaymentPlanInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<PaymentPlanInfo?>>> FetchPaymentPlanFromLoanByLoanIdAsync([FromRoute] LoanIdRequest Request)
    {
        var Result = await LoanService.FetchPaymentPlanFromLoanByLoanIdAsync(Request);

        // Si el resultado es nulo, devuelve una respuesta 404 No Encontrado
        if (Result == null)
        {
            return StatusCode(StatusCodes.Status404NotFound, Result);
        }

        return StatusCode(StatusCodes.Status200OK, Result);
    }
}
