namespace SysCredit.Api.Exceptions;

using FluentValidation.Results;

/// <summary>
///     Excepción que es lanzado cuando un objeto es validado y tiene errores.
/// </summary>
/// <param name="ValidatedInstance">
///     Objeto que se validó y tiene errores.
/// </param>
/// <param name="ValidationResult">
///    Información de los errores del objeto que se validó.
/// </param>
public class ValidationException(object ValidatedInstance, ValidationResult ValidationResult) : FluentValidation.ValidationException(ValidationResult.Errors)
{
    /// <summary>
    ///     Objeto que fue validado.
    /// </summary>
    public object ValidatedInstance { get; } = ValidatedInstance;

    /// <summary>
    ///     Información de los errores del objeto que fue validado.
    /// </summary>
    public ValidationResult ValidationResult { get; } = ValidationResult;
}
