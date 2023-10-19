namespace SysCredit.Api.Requests.Guarantors;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Guarantors;

using SysCredit.Models;

/// <summary>
///     Request para crear un <see cref="Guarantor"/>.
/// </summary>
[Validator<CreateGuarantorValidator>]
public class CreateGuarantorRequest : IRequest
{
    /// <summary>
    ///     Identificación del fiador.
    /// </summary>
    public string Identification { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del fiador.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Apellido del fiador.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    ///     Género del fiador.
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    ///     Correo electrónico del fiador.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    ///     Dirección del fiador.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    ///     Vecindario del fiador.
    /// </summary>
    public string Neighborhood { get; set; } = string.Empty;

    /// <summary>
    ///     Tipo de negocio del fiador.
    /// </summary>
    public string BussinessType { get; set; } = string.Empty;
    
    /// <summary>
    ///     Dirección del negocio del fiador.
    /// </summary>
    public string BussinessAddress { get; set; } = string.Empty;

    /// <summary>
    ///     Teléfono del fiador.
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}
