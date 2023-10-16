namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Services;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Guarantors;
using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;

/// <summary>
///     Endpoints para las distintas operaciones relacionadas al tipo de fiador
/// </summary>
/// <param name="GuarantorService">
///     Servicio que tiene toda la funcionalidad y operaciones relacionadas al Guarantor
/// </param>
/// <param name="Logger">
///     Objeto Logger para el controlador
/// </param>
[ApiController]
[Route("Api/[Controller]")]
public class GuarantorController(IGuarantorService GuarantorService, ILogger<GuarantorController> Logger) : ControllerBase
{
    /// <summary>
    ///     Endpoint para insertar un Guarantor
    /// </summary>
    /// <param name="Request">
    ///     Recibe los datos necesarios para crear un Guarantor
    /// </param>
    /// <returns>Retorna el id del Guarantor</returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(IResponse<CreateGuarantorRequest>))]
    public async Task<ActionResult<IResponse<EntityId>>> InsertGuarantorAsync([FromBody] CreateGuarantorRequest Request)
    {
        Logger.LogInformation("EndPoint[POST]: /Api/Guarantor");
        var Result = await GuarantorService.InsertGuarantorAsync(Request);
        return StatusCode(StatusCodes.Status201Created, Result);
    }

    /// <summary>
    ///     Endpoint para obtener el guarantor por su id
    /// </summary>
    /// <param name="GuarantorId">Id de Guarantor</param>
    /// <returns>Retorna el Guarantor</returns>
    [HttpGet("{GuarantorId}")]
    [ProducesResponseType(typeof(IResponse<GuarantorInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<GuarantorInfo?>>> FetchGuarantorByIdAsync(long GuarantorId)
    {
        Logger.LogInformation("Endpoint[GET]: /Api/Guarantor/{GuarantorId}", GuarantorId);
        return Ok(await GuarantorService.FetchGuarantorByIdAsync(GuarantorId).ToResponseAsync());
    }

    /// <summary>
    ///     Endpoint paara obtener una colección paginada de Guarantor.
    /// </summary>
    /// <param name="Request">
    ///     La solicitud de paginación que contiene los parámetros de paginación, como el desplazamiento (offset) y límite (limit).
    /// </param>
    /// <returns>Retorna la colección de Guarantor recuperada de acuerdo con los parámetros de paginación.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<FetchGuarantor>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<FetchGuarantor>>>> FetchGuarantorsAsync([FromQuery] PaginationRequest Request)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Guarantor");

        if (Request.Offset.HasValue && Request.Limit.HasValue)
        {
            return Ok(await GuarantorService.FetchGuarantorsAsync(Request).ToResponseAsync());
        }
        else
        {
            return Ok(await GuarantorService.FetchGuarantorsAsync().ToResponseAsync());
        }
    }

    /// <summary>
    ///     Elimina de forma asíncrona un garante especificado por su identificador.
    /// </summary>
    /// <param name="Request">
    ///     La solicitud que contiene el identificador del garante a eliminar.
    /// </param>
    /// <returns></returns>
    [HttpDelete("{GuarantorId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteGuarantorAsync([FromRoute] DeleteGuarantorRequest Request)
    {
        Logger.LogInformation("EndPoint[DELETE]: /Api/Guarantor/{GuarantorId}", Request.GuarantorId);
        await GuarantorService.DeleteGuarantorAsync(Request);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    /// <summary>
    ///     Busca Guarantor que coincidan con los criterios especificados.
    /// </summary>
    /// <param name="Request">Representa el valor que se buscará en los registros de base de datos.</param>
    /// <returns>Una acción de resultado que representa la respuesta HTTP que contiene los garantes encontrados.</returns>
    [HttpGet("Search")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<GuarantorInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<GuarantorInfo>>>> SearchGuarantorAsync([FromQuery] SearchRequest Request)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Guarantor/Search");
        return Ok(await GuarantorService.SearchGuarantorAsync(Request).ToResponseAsync());
    }
}

