namespace SysCredit.Api.Attributes;

using SysCredit.Api.Interfaces;

/// <summary>
///     Genera la interfaz de un Manager usando los métodos de instancia marcados con <see cref="MethodIdAttribute"/>.
/// </summary>
/// <typeparam name="TInterface">
///     Nombre de tipo de la interfaz que se va generar.
/// </typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ManagerInterfaceAttribute<TInterface> : Attribute where TInterface : IManager
{
    /// <summary>
    ///     Tipo de la interfaz usada por el Manager.
    /// </summary>
    public Type InterfaceType { get; } = typeof(TInterface);
}
