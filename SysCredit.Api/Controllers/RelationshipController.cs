namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
/// <param name="RelationshipService"></param>
[ApiController]
[Route("Api/[Controller]")]
public class RelationshipController(IRelationshipService RelationshipService, ILogger<RelationshipController> Logger) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("/Api/Relationships")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<RelationshipInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchRelationshipAsync()
    {
        return await RelationshipService.FetchRelationshipAsync().ToResponseAsync();
    }
}
