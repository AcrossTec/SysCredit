namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Helpers.Pagination;
using SysCredit.Api.Helpers.Search;
using SysCredit.Api.Helpers.Sorting;
using SysCredit.Api.Services;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Customer;

[ApiController]
[Route("Api/[Controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService Service;
    
    public CustomerController(ICustomerService Service)
    {
        this.Service = Service;
    }

    [HttpGet]
    public async ValueTask<ActionResult<Collection<CustomerDataTransferObject>>> GetCustomers(
        [FromQuery] PagingOptions PagingOptions,
        [FromQuery] SortOptions<CustomerOptions> SortOptions,
        [FromQuery] SearchOptions<CustomerOptions> SearchOptions)
    {
        return await Service.GetCustomersAsync(PagingOptions, SortOptions, SearchOptions);
    }

    [HttpPost]
    public ValueTask<Response<CustomerDataTransferObject>> PostCustomer([FromBody] CreateCustomer Customer)
    {
        return Service.AddCustomerAsync(Customer);
    }
}
