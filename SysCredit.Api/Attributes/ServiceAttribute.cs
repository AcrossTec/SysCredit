namespace SysCredit.Api.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ServiceAttribute : Attribute
{
    public ServiceAttribute(Type InterfaceType)
    {
        if (!InterfaceType.IsInterface)
        {
            throw new InvalidOperationException($"'{InterfaceType.FullName}' Is Not Interface");
        }

        this.InterfaceType = InterfaceType;
    }

    public Type InterfaceType { get; }
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ServiceAttribute<TInterface> : ServiceAttribute
{
    public ServiceAttribute() : base(typeof(TInterface))
    {
    }
}
