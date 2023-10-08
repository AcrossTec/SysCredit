namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Controller]
[AllowAnonymous]
[Route("Api/[Controller]")]
public class AuthenticationController() : ControllerBase
{
}
