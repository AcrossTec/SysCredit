namespace SysCredit.Models;

/// <summary>
///     Datos generales asociados a las distintas entidades de la base de datos.
/// </summary>
public abstract partial record Entity : IEntity
{
    /// <summary>
    ///     Fecha de creación del registro.
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    /// <summary>
    ///     Fecha en la que se está modificando el registro.
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    ///     Fecha en la que se está eliminando el registro.
    /// </summary>
    public DateTime? DeletedDate { get; set; }

    /// <summary>
    ///     Indica si el registro ha sido editado.
    /// </summary>
    public bool IsEdit { get; set; }

    /// <summary>
    ///     Indica si el registro ha sido eliminado.
    /// </summary>
    public bool IsDelete { get; set; }
}
