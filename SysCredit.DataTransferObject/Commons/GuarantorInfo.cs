namespace SysCredit.DataTransferObject.Commons;

using System.Text.Json.Serialization;

using static System.Text.Json.Serialization.JsonIgnoreCondition;

public record class GuarantorInfo : IDataTransferObject
{
    public long GuarantorId { get; set; }

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

    [JsonIgnore(Condition = WhenWritingNull)]
    public long? RelationshipId { get; set; }

    [JsonIgnore(Condition = WhenWritingNull)]
    public string? RelationshipName { get; set; }
}
