namespace SysCredit.Api.Attributes;

using SysCredit.Models;

/// <summary>
///     Especifica el modelo para la interfaz <see cref="Interfaces.IService{TModel}"/>.
/// </summary>
/// <typeparam name="TModel">
///     Tipo del modelo usado por el atributo.
/// </typeparam>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ServiceModelAttribute<TModel> : Attribute where TModel : Entity;
