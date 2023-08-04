namespace SysCredit.Api.DataTransferObject.StoredProcedures;

using Newtonsoft.Json;

using SysCredit.Api.DataTransferObject.Commons;
using SysCredit.Api.Enums;

public record class SearchCustomer
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

    [System.Text.Json.Serialization.JsonIgnore]
    public string JsonReferences
    {
        get => JsonConvert.SerializeObject(References);
        set => References = JsonConvert.DeserializeObject<ReferenceInfo[]>(value)!;
    }

    [System.Text.Json.Serialization.JsonIgnore]
    public string JsonGuarantors
    {
        get => JsonConvert.SerializeObject(Guarantors);
        set => Guarantors = JsonConvert.DeserializeObject<GuarantorInfo[]>(value)!;
    }

    public ReferenceInfo[] References { get; set; } = Array.Empty<ReferenceInfo>();

    public GuarantorInfo[] Guarantors { get; set; } = Array.Empty<GuarantorInfo>();
}
