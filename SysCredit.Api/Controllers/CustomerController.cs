namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.DataTransferObject.StoredProcedures;
using SysCredit.Api.Extensions;
using SysCredit.Api.Helpers;
using SysCredit.Api.Interfaces;
using SysCredit.Api.ViewModels.Customers;

[ApiController]
[Route("Api/[Controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService CustomerService;

    public CustomerController(ICustomerService CustomerService)
    {
        this.CustomerService = CustomerService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("/Api/Customers")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<FetchCustomer>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomersAsync()
    {
        return await CustomerService.FetchCustomersAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ViewModel"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreateCustomerRequest>))]
    public async Task<IActionResult> InsertCustomerAsync([FromBody] CreateCustomerRequest ViewModel)
    {
        return await CustomerService.InsertCustomerAsync(ViewModel).ToActionResultAsync(StatusCodes.Status201Created, ViewModel);
    }
}
