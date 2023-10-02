namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Authentications.Roles;
using SysCredit.Api.Requests.Authentications.Users;

using SysCredit.Helpers;

[Controller]
[Route("Api/[Controller]")]
public class AuthenticationController(IAuthenticationService AuthenticationService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("/Api/Account/Register")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreateUserRequest>))]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest Request)
    {
        var Result = await AuthenticationService.CreateUserAsync(Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }

    [AllowAnonymous]
    [HttpPost("/Api/Account/CreateRole")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreateRoleRequest>))]
    public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest Request)
    {
        var Result = await AuthenticationService.CreateRoleAsync(Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }

    [AllowAnonymous]
    [HttpPost("/Api/Auth/Token")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<TokenRequest>))]
    public async Task<IActionResult> TokenAsync([FromBody] TokenRequest Request)
    {
        var Result = await AuthenticationService.TokenRequestAsync(Request);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(Request));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }
}
