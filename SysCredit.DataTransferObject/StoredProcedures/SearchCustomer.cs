namespace SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Enums;

using System.Text.Json.Serialization;

using static Newtonsoft.Json.JsonConvert;

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

    [JsonIgnore]
    public string JsonReferences
    {
        get => SerializeObject(References);
        set => References = DeserializeObject<ReferenceInfo[]>(value)!;
    }

    [JsonIgnore]
    public string JsonGuarantors
    {
        get => SerializeObject(Guarantors);
        set => Guarantors = DeserializeObject<GuarantorInfo[]>(value)!;
    }

    public ReferenceInfo[] References { get; set; } = Array.Empty<ReferenceInfo>();

    public GuarantorInfo[] Guarantors { get; set; } = Array.Empty<GuarantorInfo>();
}
