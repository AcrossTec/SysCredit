namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Customers;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;

/// <summary>
/// 
/// </summary>
/// <param name="CustomerService"></param>
[ApiController]
[Route("Api/[Controller]")]
public class CustomerController(ICustomerService CustomerService) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("/Api/Customers")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<CustomerInfo>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomersAsync()
    {
        return await CustomerService.FetchCustomersAsync().ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [HttpGet("/Api/Customer/Search")]
    [ProducesResponseType(typeof(IResponse<IAsyncEnumerable<SearchCustomer>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> SearchCustomerAsync([FromQuery] SearchRequest Request)
    {
        return await CustomerService.SearchCustomerAsync(Request).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CustomerId"></param>
    /// <returns></returns>
    [HttpGet("/Api/Customer/{CustomerId}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomerByIdAsync(long? CustomerId)
    {
        return await CustomerService.FetchCustomerByIdAsync(CustomerId).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Identification"></param>
    /// <returns></returns>
    [HttpGet("/Api/Customer/ByIdentification/{Identification}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomerByIdentificationAsync(string? Identification)
    {
        return await CustomerService.FetchCustomerByIdentificationAsync(Identification).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Email"></param>
    /// <returns></returns>
    [HttpGet("/Api/Customer/ByEmail/{Email}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomerByEmailAsync(string? Email)
    {
        return await CustomerService.FetchCustomerByEmailAsync(Email).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Phone"></param>
    /// <returns></returns>
    [HttpGet("/Api/Customer/ByPhone/{Phone}")]
    [ProducesResponseType(typeof(IResponse<CustomerInfo>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IResponse> FetchCustomerByPhoneAsync(string? Phone)
    {
        return await CustomerService.FetchCustomerByPhoneAsync(Phone).ToResponseAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ViewModel"></param>
    /// <returns></returns>
    [HttpPost("/Api/Customer")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<EntityId>), StatusCodes.Status201Created)]
    [ProducesErrorResponseType(typeof(IResponse<CreateCustomerRequest>))]
    public async Task<IActionResult> InsertCustomerAsync([FromBody] CreateCustomerRequest ViewModel)
    {
        var Result = await CustomerService.InsertCustomerAsync(ViewModel);

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
