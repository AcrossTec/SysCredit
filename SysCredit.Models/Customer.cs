namespace SysCredit.Models;

/// <summary>
///     Datos del cliente del sistema de crédito.
/// </summary>
public record class Customer : Entity
{
    /// <summary>
    ///     Identificador único autogenerado y autoincremental por la base de datos.
    /// </summary>
    public long CustomerId { get; set; }

    /// <summary>
    ///     Identificación del cliente: DNI, Cédula, Carnet, etc...
    /// </summary>
    /// <remarks>
    ///     Este valor debe ser único.
    /// </remarks>
    public string Identification { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del cliente.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Apellido del cliente.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    ///     Género del cliente.
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    ///     Correo del cliente.
    /// </summary>
    /// <remarks>
    ///     Este valor debe ser único.
    /// </remarks>
    public string? Email { get; set; }

    /// <summary>
    ///     Dirección de donde vive el cliente.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    ///     Barrio de donde vive el cliente.
    /// </summary>
    public string Neighborhood { get; set; } = string.Empty;

    /// <summary>
    ///     Tipo de negocio que tiene el cliente.
    /// </summary>
    public string BussinessType { get; set; } = string.Empty;

    /// <summary>
    ///     Dirección del negocio del cliente.
    /// </summary>
    public string BussinessAddress { get; set; } = string.Empty;

    /// <summary>
    ///     Teléfono del cliente.
    /// </summary>
    /// <remarks>
    ///     Este valor debe ser único.
    /// </remarks>
    public string Phone { get; set; } = string.Empty;
}
