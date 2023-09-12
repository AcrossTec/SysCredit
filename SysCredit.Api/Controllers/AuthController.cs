using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.ViewModels.Auth.Roles;
using SysCredit.Api.ViewModels.Auth.Users;
using SysCredit.Helpers;

namespace SysCredit.Api.Controllers;

[Controller]
[Route("api/[Controller]")]
public class AuthController(IAuthService AuthService): ControllerBase
{
    private readonly IAuthService AuthService = AuthService;

    [AllowAnonymous]
    [HttpPost("/Api/Account/Register")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreateUserRequest>))]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest ViewModel)
    {
        var Result = await AuthService.CreateUserAsync(ViewModel);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(ViewModel));
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
    public async Task<IActionResult> CreateRoleAsync([FromBody] CreateRoleRequest ViewModel)
    {
        var Result = await AuthService.CreateRoleAsync(ViewModel);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(ViewModel));
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
    public async Task<IActionResult> TokenAsync([FromBody] TokenRequest ViewModel)
    {
        var Result = await AuthService.TokenRequestAsync(ViewModel);

        if (Result.Status.HasError)
        {
            return StatusCode(StatusCodes.Status400BadRequest, await Result.ToResponseWithReplaceDataAsync(ViewModel));
        }
        else
        {
            return StatusCode(StatusCodes.Status201Created, Result);
        }
    }
}
