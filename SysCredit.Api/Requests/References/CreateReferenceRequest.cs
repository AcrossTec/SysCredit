namespace SysCredit.Api.Requests.References;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.References;

using SysCredit.Models;

/// <summary>
///     Request para crear una Referencia
/// </summary>
[Validator<CreateReferenceValidator>]
public class CreateReferenceRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el Id de la referencia
    /// </summary>
    public string? Identification { get; set; }

    /// <summary>
    ///     Propiedad que representa el Nombre de la referencia
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Propiedad que representa el Apellido de la referencia
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    ///     Propiedad que representa el Genero de la referencia
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    ///     Propiedad que representa el Numero de telefono de la referencia
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    ///     Propiedad que representa el Correo Electronico de la referencia
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    ///     Propiedad que representa la Direccion de la referencia
    /// </summary>
    public string? Address { get; set; }
}
