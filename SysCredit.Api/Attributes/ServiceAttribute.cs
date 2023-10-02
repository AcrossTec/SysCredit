namespace SysCredit.Api.Attributes;

/// <summary>
///     Marca un objeto como un servicio y define la creacion de su interfaz y los métodos que esten marcados con <see cref="MethodIdAttribute "/>.
/// </summary>
/// <typeparam name="TInterface">
///     Tipo de la interfaz que se va ha crear.
/// </typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ServiceAttribute<TInterface> : Attribute
{
    /// <summary>
    ///     Construye un metadato para una clase de servicio.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    ///     Lanzada cuando <typeparamref name="TInterface" /> no es una interfaz.
    /// </exception>
    public ServiceAttribute()
    {
        InterfaceType = typeof(TInterface);

        if (!InterfaceType.IsInterface)
        {
            throw new InvalidOperationException($"'{InterfaceType.FullName}' Is Not Interface");
        }
    }

    /// <summary>
    ///     Tipo de la interfaz usanda por el servicio.
    /// </summary>
    public Type InterfaceType { get; }
}
