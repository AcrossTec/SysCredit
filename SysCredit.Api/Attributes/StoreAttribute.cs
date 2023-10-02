namespace SysCredit.Api.Attributes;

/// <summary>
///     Marca una clase como un Store o tienda de datos que acceden a la base de datos.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public partial class StoreAttribute : Attribute;