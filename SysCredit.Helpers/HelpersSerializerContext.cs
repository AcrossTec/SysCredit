namespace SysCredit.Helpers;

using System.Text.Json.Serialization;

[JsonSerializable(typeof(EntityId))]
[JsonSerializable(typeof(ErrorStatus))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSerializable(typeof(Response))]
[JsonSerializable(typeof(Response<EntityId>))]
[JsonSerializable(typeof(Response<ErrorResponse>))]
public partial class HelpersSerializerContext : JsonSerializerContext
{
}
