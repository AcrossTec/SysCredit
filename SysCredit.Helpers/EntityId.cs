namespace SysCredit.Helpers;

/// <summary>
///     Objeto usado para retornar el Id de la entidad que se está creando.
/// </summary>
public record struct EntityId
{
    /// <summary>
    ///     Id único de la entidad creada desde base de datos.
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    ///     Convierte un <see cref="EntityId" /> en un <see cref="long" />?.
    /// </summary>
    /// <param name="EntityId">
    ///     <see cref="EntityId" /> que se va ha convertir en <see cref="long" />?.
    /// </param>
    public static implicit operator long?(EntityId EntityId) => EntityId.Id;

    /// <summary>
    ///     Convierte un <see cref="long" />? en un <see cref="EntityId" />.
    /// </summary>
    /// <param name="Id">
    ///     <see cref="long" />? que se va ha convertir en un <see cref="EntityId" />.
    /// </param>
    public static implicit operator EntityId(long? Id) => new() { Id = Id };
}
