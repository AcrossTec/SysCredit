namespace SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.DataTransferObject.Commons;

using System.Text.Json;
using System.Text.Json.Serialization;

public record class SearchCustomer
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

    [JsonIgnore]
    public string JsonReferences
    {
        get => JsonSerializer.Serialize(References, SysCreditDataTransferObjectSerializerContext.Default.ReferenceInfoArray);
        set => References = JsonSerializer.Deserialize(value, SysCreditDataTransferObjectSerializerContext.Default.ReferenceInfoArray) ?? [];
    }

    [JsonIgnore]
    public string JsonGuarantors
    {
        get => JsonSerializer.Serialize(Guarantors, SysCreditDataTransferObjectSerializerContext.Default.GuarantorInfoArray);
        set => Guarantors = JsonSerializer.Deserialize(value, SysCreditDataTransferObjectSerializerContext.Default.GuarantorInfoArray) ?? [];
    }

    public ReferenceInfo[] References { get; set; } = [];

    public GuarantorInfo[] Guarantors { get; set; } = [];
}
