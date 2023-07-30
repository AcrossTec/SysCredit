namespace SysCredit.Api.ViewModels.Customers;

using SysCredit.Api.ViewModels.Guarantors;
using SysCredit.Api.ViewModels.References;

public class CreateCustomerViewModel
{
    public string Identification { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string BussinessType { get; set; } = string.Empty;

    public string BussinessAddress { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public CreateGuarantorViewModel[] Guarantors { get; set; } = Array.Empty<CreateGuarantorViewModel>();

    public CreateReferenceViewModel[] References { get; set; } = Array.Empty<CreateReferenceViewModel>();
}
