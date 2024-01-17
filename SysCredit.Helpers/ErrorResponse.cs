namespace SysCredit.Helpers;

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

/// <summary>
///     Modelo de respuesta usado por el Middleware de ASP NET cuando se captura un error.
/// </summary>
/// <param name="HttpMethod">
///     Verbo Http que ejecutó el Endpoint: GET, POST, PUT, DELETE, PATCH.
/// </param>
/// <param name="ServerHost">
///     Url del servidor.
/// </param>
/// <param name="EndpointPath">
///     Ruta del recurso que se solicitó en el Endpoint.
/// </param>
/// <param name="QueryString">
///     Paramétros usados en las peticiones, normalmente GET.
/// </param>
/// <param name="RequestBody">
///     Objeto de petición que un Endpoint con el verbo POST, PUT, PATCH envía desde algún cliente.
/// </param>
public record class ErrorResponse(
    string HttpMethod, string ServerHost, string EndpointPath,
   [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] string? QueryString,
   [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] JsonNode? RequestBody);
