namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.LoanType;
using SysCredit.Api.Requests.LoanTypes;
using SysCredit.Helpers;
using SysCredit.Models;

/// <summary>
///     Endpoints para las distintas operaciones relacionadas al tipo de prestamo
/// </summary>
/// <param name="LoanTypeService">
///     Servicio que tiene toda la funcionalidad y operaciones relacionadas al tipo de prestamo
/// </param>
/// <param name="Logger">
///     Objeto Logger para el controlador
/// </param>
[ApiController]
[Route("Api/[Controller]")]
public class LoanTypeController(ILoanTypeService LoanTypeService, ILogger<LoanTypeController> Logger) : ControllerBase
{
    /// <summary>
    ///     Endpoint para obtener todos los tipos de prestamo
    /// </summary>
    /// <returns>Regresa una lista de tipos de prestamo</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanTypeInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<LoanTypeInfo>>>> FetchLoanTypeAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/LoanType");
        return Ok(await LoanTypeService.FetchLoanTypeAsync().ToResponseAsync());
    }

    /// <summary>
    ///     Endpoint para obtener todos los registro de tipo de prestamo sin procesar su información
    /// </summary>
    /// <returns>Retorna una lista de tipos de prestamo</returns>
    [HttpGet("Complete")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<LoanType>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<LoanType>>>> FetchLoanTypeCompleteAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/LoanType/Complete");
        return Ok(await LoanTypeService.FetchLoanTypeCompleteAsync().ToResponseAsync());
    }

    /// <summary>
    ///     Endpoint para obtener el tipo de prestamo por su id
    /// </summary>
    /// <param name="LoanTypeId">
    ///     Identificador del tipo de prestamo
    /// </param>
    /// <returns>Retorna el registro que coincida con el identificador del tipo de prestamo</returns>
    [HttpGet("{LoanTypeId}")]
    [ProducesResponseType(typeof(IResponse<LoanTypeInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<LoanTypeInfo>>> FetchLoanTypeByIdAsync([FromRoute] long LoanTypeId)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/LoanType/{LoanTypeId}", LoanTypeId);
        return Ok(await LoanTypeService.FetchLoanTypeByIdAsync(LoanTypeId).ToResponseAsync());
    }

    /// <summary>
    ///     Endpoint para borrar un tipo de prestamo
    /// </summary>
    /// <param name="Request">
    ///     Id del <see cref="Models.LoanType"/> a eliminar
    /// </param>
    /// <returns></returns>
    [HttpDelete("{LoanTypeId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteLoanTypeAsync([FromRoute] DeleteLoanTypeRequest Request)
    {
        Logger.LogInformation("EndPoint[DELETE]: /Api/LoanType/{LoanTypeId}", Request.LoanTypeId);
        await LoanTypeService.DeleteLoanTypeAsync(Request);
        return NoContent();
    }

    /// <summary>
    ///     Endpoint para insertar un tipo de prestamo
    /// </summary>
    /// <param name="Request">Recibe el nombre unico para crear el tipo de prestamo </param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<EntityId>>> InsertLoanTypeAsync([FromBody] CreateLoanTypeRequest Request)
    {
        Logger.LogInformation("EndPoint[POST]: /Api/LoanType");
        var Result = await LoanTypeService.InsertLoanTypeAsync(Request).ToResponseAsync();
        return StatusCode(StatusCodes.Status201Created, Result);
    }

    /// <summary>
    ///     Endpoint para actualizar un tipo de prestamo por su id
    /// </summary>
    /// <param name="Request">Obtien el nombre y el id del prestamo</param>
    /// <returns></returns>
    [HttpPut("{LoanTypeId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateLoanTypeAsync([FromBody] UpdateLoanTypeRequest Request)
    {
        Logger.LogInformation("EndPoint[PUT]: /Api/LoanType/{LoanTypeId}", Request.LoanTypeId);
        await LoanTypeService.UpdateLoanTypeAsync(Request);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpGet("TestLoanType")]
    public async Task<ActionResult<IResponse<dynamic>>> TestFetchLoanType()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/TestLoanType");
        return Ok(await LoanTypeService.TestFetchLoanType().ToResponseAsync());
    }
}
