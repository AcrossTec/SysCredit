namespace SysCredit.Api.Requests.Customers;

using SysCredit.Api.Attributes;
using SysCredit.Api.Requests.References;
using SysCredit.Api.Validations.Customers;

using SysCredit.Models;

/// <summary>
///     Clase que representa la solicitud para crear un cliente.
/// </summary>
[Validator<CreateCustomerValidator>]
public class CreateCustomerRequest : IRequest
{
    /// <summary>
    ///     Obtiene o establece la identificación del cliente.
    /// </summary>
    public string Identification { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece el nombre del cliente.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece el apellido del cliente.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece el género del cliente.
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    ///     Obtiene o establece el correo electrónico del cliente.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    ///     Obtiene o establece la dirección del cliente.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece el vecindario del cliente.
    /// </summary>
    public string Neighborhood { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece el tipo de negocio del cliente.
    /// </summary>
    public string BussinessType { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece la dirección del negocio del cliente.
    /// </summary>
    public string BussinessAddress { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece el teléfono del cliente.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    ///     Obtiene o establece una lista de los fiadores del cliente.
    /// </summary>
    public CustomerGuarantorRequest[] Guarantors { get; set; } = [];

    /// <summary>
    ///     Obtiene o establece una lista de las referencias del cliente.
    /// </summary>
    public CreateReferenceRequest[] References { get; set; } = [];
}
