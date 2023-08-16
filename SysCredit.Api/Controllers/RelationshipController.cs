namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;

using SysCredit.Helpers;

[ApiController]
[Route("Api/[Controller]")]
public class RelationshipController : ControllerBase
{
    private readonly IRelationshipService RelationshipService;

    public RelationshipController(IRelationshipService RelationshipService)
    {
        this.RelationshipService = RelationshipService;
    }

    [HttpGet("/Api/Relationships")]
    public async Task<IResponse> FetchRelationshipAsync()
    {
        return await RelationshipService.FetchRelationshipAsync().ToResponseAsync();
    }
}
