namespace SysCredit.Api.DataTransferObject;

public record class CustomerDataTransferObject
{
    public long CustomerId { get; set; }

    public string? Identification { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Neighborhood { get; set; }

    public string? BussinessType { get; set; }

    public string? BussinessAddress { get; set; }

    public string? Phone { get; set; }

    public IEnumerable<CustomerReferenceDataTransferObject> Relationships { get; set; } = Enumerable.Empty<CustomerReferenceDataTransferObject>();

    public IEnumerable<CustomerGuarantorDataTransferObject> Guarantors { get; set; } = Enumerable.Empty<CustomerGuarantorDataTransferObject>();
}
