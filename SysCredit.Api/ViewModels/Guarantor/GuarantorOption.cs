using SysCredit.Api.Helpers.Atributtes;

namespace SysCredit.Api.ViewModels.Guarantor;

public class GuarantorOption
{
    [Sortable]
    [Searchable]
    public long GuarantorId { get; set; }
    
    [Sortable]
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

    [Sortable]
    [Searchable]
    public string? Relationship { get; set; }
}