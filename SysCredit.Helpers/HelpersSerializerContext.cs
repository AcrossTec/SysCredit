namespace SysCredit.Helpers;

using System.Text.Json.Serialization;

[JsonSerializable(typeof(EntityId))]
[JsonSerializable(typeof(ErrorStatus))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSerializable(typeof(Response))]
[JsonSerializable(typeof(Response<EntityId>))]
[JsonSerializable(typeof(Response<ErrorResponse>))]
[JsonSerializable(typeof(Dictionary<string, string>))]
public partial class HelpersSerializerContext : JsonSerializerContext
{
}
