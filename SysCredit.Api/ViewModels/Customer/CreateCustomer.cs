using SysCredit.Api.ViewModels.Guarantor;
using SysCredit.Api.ViewModels.Reference;

namespace SysCredit.Api.ViewModels.Customer;

public record class CreateCustomer
{
    public string Identification { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string BussinessType { get; set; } = string.Empty;

    public string BussinessAddress { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;
    
    public IEnumerable<CustomerGuarantor> Guarantors { get; set; } = Enumerable.Empty<CustomerGuarantor>();

    public IEnumerable<CustomerReference> References { get; set; } = Enumerable.Empty<CustomerReference>();
}
