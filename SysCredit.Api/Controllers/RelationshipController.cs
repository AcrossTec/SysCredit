namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Relationships;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

/// <summary>
///     Endpoints para las distintas operaciones con el catálogo <see cref="Models.Relationship"/>.
/// </summary>
/// <param name="RelationshipService">
///     Servico que provee las opeeraciones básicas para la tabla <see cref="Models.Relationship"/>.
/// </param>
[ApiController]
[Route("Api/[Controller]")]
public class RelationshipController(IRelationshipService RelationshipService, ILogger<RelationshipController> Logger) : ControllerBase
{
    /// <summary>
    ///     Regresa todos los registros de la tabla <see cref="Models.Relationship"/>.
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
    ///     Datos que se van ha actualizar del <see cref="Models.Relationship"/>.
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
    /// 
    /// </summary>
    /// <param name="RelationshipId"></param>
    /// <returns></returns>
    [HttpGet("{RelationshipId}")]
    [ProducesResponseType(typeof(IResponse<RelationshipInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<RelationshipInfo?>> FetchRelationshipByIdIdAsync([FromRoute] long RelationshipId)
    {
        return await RelationshipService.FetchRelationshipByIdAsync(RelationshipId).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse<long>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<long>> InsertRelationship([FromBody] CreateRelationshipRequest Request)
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Relationship");
        return await RelationshipService.InsertRelationshipAsync(Request).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="RelationshipId"></param>
    /// <returns></returns>
    [HttpDelete("{RelationshipId}")]
    [ProducesResponseType(typeof(IResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<bool>> DeleteRelationship(long RelationshipId)
    {
        return await RelationshipService.DeleteRelationship(RelationshipId).ToResponseAsync();
    }
}
