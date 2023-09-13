namespace SysCredit.Api.Attributes;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TInterface"></typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ServiceAttribute<TInterface> : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public ServiceAttribute()
    {
        InterfaceType = typeof(TInterface);

        if (!InterfaceType.IsInterface)
        {
            throw new InvalidOperationException($"'{InterfaceType.FullName}' Is Not Interface");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public Type InterfaceType { get; }
}
