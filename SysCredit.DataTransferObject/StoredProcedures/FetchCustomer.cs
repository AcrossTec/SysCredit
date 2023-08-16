namespace SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Enums;

public record class FetchCustomer
{
    public long CustomerId { get; set; }

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

    public long ReferenceId { get; set; }

    public string? ReferenceIdentification { get; set; }

    public string ReferenceName { get; set; } = string.Empty;

    public string ReferenceLastName { get; set; } = string.Empty;

    public Gender ReferenceGender { get; set; }

    public string ReferencePhone { get; set; } = string.Empty;

    public string? ReferenceEmail { get; set; }

    public string? ReferenceAddress { get; set; }

    public long GuarantorId { get; set; }

    public string GuarantorIdentification { get; set; } = string.Empty;

    public string GuarantorName { get; set; } = string.Empty;

    public string GuarantorLastName { get; set; } = string.Empty;

    public Gender GuarantorGender { get; set; }

    public string? GuarantorEmail { get; set; }

    public string GuarantorAddress { get; set; } = string.Empty;

    public string GuarantorNeighborhood { get; set; } = string.Empty;

    public string GuarantorBussinessType { get; set; } = string.Empty;

    public string GuarantorBussinessAddress { get; set; } = string.Empty;

    public string GuarantorPhone { get; set; } = string.Empty;

    public long GuarantorRelationshipId { get; set; }

    public string GuarantorRelationshipName { get; set; } = string.Empty;
}
