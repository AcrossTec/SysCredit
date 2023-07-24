namespace SysCredit.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Services;
using SysCredit.Api.ViewModels;

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
    public ValueTask<Response<IAsyncEnumerable<CustomerDataTransferObject>>> GetCustomers()
    {
        return Service.GetCustomersAsync();
    }

    [HttpPost]
    public ValueTask<Response<CustomerDataTransferObject>> PostCustomer([FromBody] CreateCustomer Customer)
    {
        return Service.AddCustomerAsync(Customer);
    }
}
