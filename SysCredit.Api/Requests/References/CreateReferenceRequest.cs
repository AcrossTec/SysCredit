namespace SysCredit.Api.Requests.References;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.References;

using SysCredit.Models;

/// <summary>
///     Request para crear una referencia al cliente.
/// </summary>
[Validator<CreateReferenceValidator>]
public class CreateReferenceRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el Id de la referencia del cliente.
    /// </summary>
    public string? Identification { get; set; }

    /// <summary>
    ///     Propiedad que representa el Nombre de la referencia del cliente.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Propiedad que representa el Apellido de la referencia del cliente.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    ///     Propiedad que representa el Género de la referencia del cliente.
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    ///     Propiedad que representa el Número de teléfono de la referencia del cliente.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    ///     Propiedad que representa el Correo Electrónico de la referencia del cliente.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    ///     Propiedad que representa la Dirección domiciliar de la referencia del cliente.
    /// </summary>
    public string? Address { get; set; }
}
