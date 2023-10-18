namespace SysCredit.Api.Requests.Customers;

using SysCredit.Api.Attributes;
using SysCredit.Api.Requests.References;
using SysCredit.Api.Validations.Customers;

using SysCredit.Models;

/// <summary>
///     Este Request recibe todo los parametros para Crear un Cliente.
/// </summary>
[Validator<CreateCustomerValidator>]
public class CreateCustomerRequest : IRequest
{
    public string Identification { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public string? Email { get; set; }

    public string Address { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string BussinessType { get; set; } = string.Empty;

    public string BussinessAddress { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public CustomerGuarantorRequest[] Guarantors { get; set; } = Array.Empty<CustomerGuarantorRequest>();

    public CreateReferenceRequest[] References { get; set; } = Array.Empty<CreateReferenceRequest>();
}
