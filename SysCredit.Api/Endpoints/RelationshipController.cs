namespace SysCredit.Api.Endpoints;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Relationships;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

/// <summary>
///     Endpoints para las distintas operaciones relacionadas con el catálogo <see cref="Models.Relationship"/>.
/// </summary>
/// <param name="RelationshipService">
///     Servico que provee las operaciones básicas para la tabla <see cref="Models.Relationship"/>.
/// </param>
/// <param name="Logger">
///     Objeto ILogger para el controlador
/// </param>
[ApiController]
[Route("Api/[Controller]")]
public class RelationshipController(IRelationshipManager RelationshipService, ILogger<RelationshipController> Logger) : ControllerBase
{
    /// <summary>
    ///     Obtiene todos los registros de la tabla <see cref="Models.Relationship"/>.
    /// </summary>
    /// <remarks>
    ///     Endpoint    : GET /Api/Relationship
    ///     Query String: N/A
    ///     Request     : N/A
    ///     Response SUCCESS:
    ///     <code>
    ///     {
    ///         Status: {
    ///             HasError: false
    ///         },
    ///         Data: [
    ///             {
    ///                 RelationshipId: Number
    ///                 Name: String
    ///             },
    ///             ...
    ///         ]
    ///     }
    ///     </code>
    ///     Response ERROR:
    ///     <code>
    ///     {
    ///         Status: {
    ///             HasError: true,
    ///             ...
    ///         }
    ///     }
    ///     </code>
    /// </remarks>
    /// <returns>
    ///     Regresa todos los registros de la tabla <see cref="Models.Relationship"/>.
    /// </returns>
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<RelationshipInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<IAsyncEnumerable<RelationshipInfo>>>> FetchRelationshipAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Relationship");
        return Ok(await RelationshipService.FetchRelationshipAsync().ToResponseAsync());
    }

    /// <summary>
    ///     Actualiza un registro de la tabla <see cref="Models.Relationship"/>.
    /// </summary>
    /// <param name="Request">
    ///     Datos que se van a actualizar del <see cref="Models.Relationship"/>.
    /// </param>
    /// <returns>
    ///     Regresa una un Http 204.
    /// </returns>
    [HttpPut("{RelationshipId}")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateRelationshipAsync(UpdateRelationshipRequest Request)
    {
        Logger.LogInformation("EndPoint[PUT]: /Api/Relationship/{RelationshipId}", Request.RelationshipId);
        await RelationshipService.UpdateRelationshipAsync(Request);
        return NoContent();
    }

    /// <summary>
    ///     Obtiene un registro de la tabla <see cref="Models.Relationship"/>
    /// </summary>
    /// <param name="RelationshipId">
    ///     Id obtenido de la ruta
    /// </param>
    /// <returns>
    ///     Regresa un registro de la tabla <see cref="Models.Relationship"/>
    /// </returns>
    [HttpGet("{RelationshipId}")]
    [ProducesResponseType(typeof(IResponse<RelationshipInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<RelationshipInfo?>>> FetchRelationshipByIdIdAsync([FromRoute] long RelationshipId)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Relationship/{RelationshipId}", RelationshipId);
        return Ok(await RelationshipService.FetchRelationshipByIdAsync(RelationshipId).ToResponseAsync());
    }

    /// <summary>
    ///     Crear nuevo Parentesco en la base de datos
    /// </summary>
    /// <param name="Request">
    ///     Datos usado para crear el Parentesco
    /// </param>
    /// <returns>
    ///     Regresa el nuevo Id del Parentesco creado
    /// </returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IResponse<EntityId>>> InsertRelationship([FromBody] CreateRelationshipRequest Request)
    {
        Logger.LogInformation("EndPoint[POST]: /Api/Relationship");
        var Result = await RelationshipService.InsertRelationshipAsync(Request).ToResponseAsync();
        return StatusCode(StatusCodes.Status201Created, Result);
    }

    /// <summary>
    ///     Elimina un registro de la tabla <see cref="Models.Relationship"/>
    /// </summary>
    /// <param name="Request">
    ///     Id del <see cref="Models.Relationship"/> a eliminar
    /// </param>
    /// <returns>
    ///     Retorna un Http 204
    /// </returns>
    [HttpDelete("{RelationshipId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRelationshipAsync([FromRoute] DeleteRelationshipRequest Request)
    {
        Logger.LogInformation("EndPoint[DELETE]: /Api/Relationship/{RelationshipId}", Request.RelationshipId);
        await RelationshipService.DeleteRelationshipAsync(Request);
        return NoContent();
    }
}
