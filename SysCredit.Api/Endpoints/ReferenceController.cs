namespace SysCredit.Api.Endpoints;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// 
/// </summary>
[ApiController]
[Route("Api/[Controller]")]
#pragma warning disable CS9113 // Parameter is unread.
public class ReferenceController(ILogger<ReferenceController> Logger) : ControllerBase;
#pragma warning restore CS9113 // Parameter is unread.