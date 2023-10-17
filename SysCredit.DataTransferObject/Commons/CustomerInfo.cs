namespace SysCredit.DataTransferObject.Commons;

public record class CustomerInfo : IDataTransferObject
{
    public long CustomerId { get; set; }

    public string Identification { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public bool Gender { get; set; }

    public string? Email { get; set; }

    public string Address { get; set; } = string.Empty;

    public string Neighborhood { get; set; } = string.Empty;

    public string BussinessType { get; set; } = string.Empty;

    public string BussinessAddress { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public IEnumerable<ReferenceInfo> References { get; set; } = Array.Empty<ReferenceInfo>();

    public IEnumerable<CustomerGuarantorInfo> Guarantors { get; set; } = Array.Empty<CustomerGuarantorInfo>();
}
