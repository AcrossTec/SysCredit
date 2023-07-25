using SysCredit.Api.Helpers.Atributtes;
using SysCredit.Api.ViewModels.Guarantor;
using SysCredit.Api.ViewModels.Reference;

namespace SysCredit.Api.ViewModels.Customer;

public record class CustomerOptions
{
    [Sortable]
    [Searchable]
    public long CustomerId{ get; set; }

    [Sortable(Default = true)]
    [Searchable]
    public string? Identification { get; set; }

    [Sortable]
    [Searchable]
    public string? Name { get; set; }

    [Sortable]
    [Searchable]
    public string? LastName { get; set; }

    [Sortable]
    [Searchable]
    public string? Address { get; set; }

    [Sortable]
    [Searchable]
    public string? Neighborhood { get; set; }

    [Sortable]
    [Searchable]
    public string? BussinessType { get; set; }

    [Sortable]
    [Searchable]
    public string? BussinessAddress { get; set; }

    [Sortable]
    [Searchable]
    public string? Phone { get; set; }

    public IEnumerable<CustomerGuarantor> Guarantors { get; set; } = Enumerable.Empty<CustomerGuarantor>();

    public IEnumerable<CustomerReference> Relationships { get; set; } = Enumerable.Empty<CustomerReference>();
}