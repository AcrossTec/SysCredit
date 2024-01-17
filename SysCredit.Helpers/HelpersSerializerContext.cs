namespace SysCredit.Helpers;

using System.Text.Json.Serialization;

[JsonSerializable(typeof(EntityId))]
[JsonSerializable(typeof(ErrorStatus))]
[JsonSerializable(typeof(ErrorResponse))]
[JsonSerializable(typeof(Response))]
public partial class HelpersSerializerContext : JsonSerializerContext
{
    // TODO: Crear Source Generator para la versión genérica de [JsonSerializable(typeof(Response<>))]
}
