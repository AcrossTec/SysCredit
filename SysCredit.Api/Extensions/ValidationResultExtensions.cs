namespace SysCredit.Api.Extensions;

using FluentValidation.Results;

/// <summary>
///     Métodos extensiones para mejor control sobre los errores en FluentValidation.
/// </summary>
public static class ValidationResultExtensions
{
    /// <summary>
    ///     Verifica si <paramref name="Result" /> tiene errores.
    /// </summary>
    /// <param name="Result">
    ///     Resultado que se va ha analizar.
    /// </param>
    /// <returns>
    ///     Regresa True si hay errores y False si no hay errores.
    /// </returns>
    public static bool HasError(this ValidationResult Result)
    {
        return Result.IsValid is false;
    }

    /// <summary>
    ///     Convierte una lista de errores en un Diccionario.
    /// </summary>
    /// <param name="Result">
    ///     Objeto que tiene la lista de errores ha convertir.
    /// </param>
    /// <returns>
    ///     Regresa un diccionario de errores.
    /// </returns>
    public static IDictionary<string, object?> ErrorsToDictionary(this ValidationResult Result)
    {
        return Result.Errors.GroupBy(Error => Error.PropertyName)
                     .ToDictionary<IGrouping<string, ValidationFailure>, string, object?>(
                        Property => Property.Key,
                        Property => Property.Select(Property => Property.ErrorMessage).ToArray());
    }

    /// <summary>
    ///     Convierte una lista de errores en un Diccionario.
    /// </summary>
    /// <param name="Result">
    ///     Objeto que tiene la lista de errores ha convertir.
    /// </param>
    /// <returns>
    ///     Regresa un diccionario de errores con su respectivo cóðigo de error.
    /// </returns>
    public static IDictionary<string, dynamic?> ErrorsToDictionaryWithErrorCode(this ValidationResult Result)
    {
        return Result.Errors.GroupBy(Error => Error.PropertyName)
                     .ToDictionary<IGrouping<string, ValidationFailure>, string, object?>(
                        Property => Property.Key,
                        Property => Property.Select(Property => new { Property.ErrorCode, Property.ErrorMessage }).ToArray());
    }
}
