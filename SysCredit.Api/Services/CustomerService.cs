namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces;

[Service<ICustomerService>]
public class CustomerService : ICustomerService
{
}
