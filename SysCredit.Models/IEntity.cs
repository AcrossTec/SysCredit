namespace SysCredit.Models;

/// <summary>
///     Interfaz base de todas las entidades.
/// </summary>
public partial interface IEntity : IModel
{
    /// <summary>
    ///     Fecha en la que se está creando la entidad.
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    ///     Fecha en la que se está modificando la entidad.
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    ///     Fecha en la que se está borrando la entidad.
    /// </summary>
    public DateTime? DeletedDate { get; set; }

    /// <summary>
    ///     Especifica si la entidad ha sido modificada.
    /// </summary>
    public bool IsEdit { get; set; }

    /// <summary>
    ///     Especifica si la entidad ha sido borrada.
    /// </summary>
    public bool IsDelete { get; set; }
}
