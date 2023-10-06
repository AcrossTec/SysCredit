namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
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
    [HttpGet]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<RelationshipInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchRelationshipAsync()
    {
        Logger.LogInformation("EndPoint[GET]: /Api/Relationship");
        return await RelationshipService.FetchRelationshipAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="LoanTypeId"></param>
    /// <returns></returns>
    [HttpGet("/Api/Relationship/{LoanTypeId}")]
    [ProducesResponseType(typeof(IResponse<RelationshipInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(IResponse<ErrorResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse<RelationshipInfo?>> FetchRelationshipByLoanTypeIdAsync([FromRoute] long LoanTypeId)
    {
        return await RelationshipService.FetchRelationshipByLoanTypeIdAsync(LoanTypeId).ToResponseAsync();
    }
}
