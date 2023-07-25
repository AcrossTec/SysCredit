using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Services;

namespace SysCredit.Api.Controllers;

[ApiController]
[Route("Api/[Controller]")]
public class GuarantorController : ControllerBase
{
    private readonly IGuarantorServices Service;

    public GuarantorController(IGuarantorServices Service)
    {
        this.Service = Service;
    }

    [HttpGet]
    public void GetGuarantor()
    {
    }

    [HttpPost]
    public void PostGuarantor()
    {
        
    }
}
