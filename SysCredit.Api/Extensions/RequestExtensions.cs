namespace SysCredit.Api.Extensions;

using Dapper;

using FluentValidation;
using FluentValidation.Results;

using SysCredit.Api.Attributes;
using SysCredit.Api.Requests;

using System.Data;
using System.Reflection;

using ValidationException = Exceptions.ValidationException;

/// <summary>
///     Métodos de utilería para las validaciones y obtención de respuestas sobre un <see cref="IRequest" />.
/// </summary>
public static class RequestExtensions
{
    /// <summary>
    ///     Realiza una validación sobre el objeto <paramref name="Request" />.
    /// </summary>
    /// <param name="Request">
    ///     Objeto que será validado usando alguna clase derivada de <see cref="AbstractValidator{T}" /> que tenga configurada mediente el atributo <see cref="ValidatorAttribute{TValidator}" />.
    /// </param>
    /// <param name="ContextData">
    ///     Datos adicionales usados por la validación.
    /// </param>
    /// <param name="Cancellation">
    ///     Permite cancelar una validación si está en algún proceso asincrono.
    /// </param>
    /// <returns>
    ///     Regresa los resultados de validar el objeto <paramref name="Request" />.
    /// </returns>
    public static ValidationResult Validate(this IRequest Request, IDictionary<string, object>? ContextData = null)
    {
        Type ValidatorType = Request.LookupGenericTypeArgumentsFromGenericAttribute(typeof(ValidatorAttribute<>))![0];
        IValidator Validator = Activator.CreateInstance(ValidatorType).As<IValidator>()!;
        IValidationContext Context = Activator.CreateInstance(typeof(ValidationContext<>).MakeGenericType(Request.GetType()), new object[] { Request }).As<IValidationContext>()!;
        Context.RootContextData.AddRange(ContextData.DefaultIfNullOrEmpty());
        return Validator.Validate(Context);
    }

    /// <summary>
    ///     Realiza una validación sobre el objeto <paramref name="Request" />.
    /// </summary>
    /// <param name="Request">
    ///     Objeto que será validado usando alguna clase derivada de <see cref="AbstractValidator{T}" /> que tenga configurada mediente el atributo <see cref="ValidatorAttribute{TValidator}" />.
    /// </param>
    /// <param name="ContextData">
    ///     Datos adicionales usados por la validación.
    /// </param>
    /// <param name="Cancellation">
    ///     Permite cancelar una validación si está en algún proceso asincrono.
    /// </param>
    /// <returns>
    ///     Regresa los resultados de validar el objeto <paramref name="Request" />.
    /// </returns>
    public static async ValueTask<ValidationResult> ValidateAsync(this IRequest Request, IDictionary<string, object>? ContextData = null, CancellationToken Cancellation = default)
    {
        Type ValidatorType = Request.LookupGenericTypeArgumentsFromGenericAttribute(typeof(ValidatorAttribute<>))![0];
        IValidator Validator = Activator.CreateInstance(ValidatorType).As<IValidator>()!;
        IValidationContext Context = Activator.CreateInstance(typeof(ValidationContext<>).MakeGenericType(Request.GetType()), new object[] { Request }).As<IValidationContext>()!;
        Context.RootContextData.AddRange(ContextData.DefaultIfNullOrEmpty());
        return await Validator.ValidateAsync(Context, Cancellation);
    }

    /// <summary>
    ///     Valida el <paramref name="Request" /> usando el validador configurado mediante el atributo <see cref="ValidatorAttribute{TValidator}" />.
    /// </summary>
    /// <param name="Request">
    ///     Objeto que será validado.
    /// </param>
    /// <param name="ContextData">
    ///     Datos adicionales usados por la validación del request.
    /// </param>
    /// <param name="Cancellation">
    ///     Permite cancelar una validación si está en un proceso asincrono.
    /// </param>
    /// <exception cref="ValidationException">
    ///     Excepción que es lanzado cuando el resultado de la validación tiene errores.
    /// </exception>
    public static void ValidateAndThrowOnFailures<TRequest>(this TRequest Request, IDictionary<string, object>? ContextData = null) where TRequest : IRequest
    {
        var ValidationResult = Validate(Request, ContextData);

        if (ValidationResult.HasError())
        {
            throw new ValidationException(Request, ValidationResult);
        }
    }

    /// <summary>
    ///     Valida el <paramref name="Request" /> usando el validador configurado mediante el atributo <see cref="ValidatorAttribute{TValidator}" />.
    /// </summary>
    /// <param name="Request">
    ///     Objeto que será validado.
    /// </param>
    /// <param name="ContextData">
    ///     Datos adicionales usados por la validación del request.
    /// </param>
    /// <param name="Cancellation">
    ///     Permite cancelar una validación si está en un proceso asincrono.
    /// </param>
    /// <exception cref="ValidationException">
    ///     Excepción que es lanzado cuando el resultado de la validación tiene errores.
    /// </exception>
    public static async ValueTask ValidateAndThrowOnFailuresAsync<TRequest>(this TRequest Request, IDictionary<string, object>? ContextData = null, CancellationToken Cancellation = default) where TRequest : IRequest
    {
        var ValidationResult = await ValidateAsync(Request, ContextData, Cancellation);

        if (ValidationResult.HasError())
        {
            throw new ValidationException(Request, ValidationResult);
        }
    }

    /// <summary>
    ///     Convierte el <paramref name="Request" /> en un objeto de tipo <see cref="DynamicParameters" />.
    /// </summary>
    /// <param name="Request">
    ///     Objeto que se convertirá en un <see cref="DynamicParameters" />.
    /// </param>
    /// <returns>
    ///     Regresa un <see cref="DynamicParameters" /> como resultado de la conversión de <paramref name="Request" />.
    /// </returns>
    public static DynamicParameters ToDynamicParameters(this IRequest Request)
    {
        var Parameters = new DynamicParameters();

        PropertyInfo[] Properties = Request.GetType().GetProperties();

        foreach (var Property in Properties)
        {
            switch (Property.GetValue(Request))
            {
                case IEnumerable<IRequest> Values:
                {
                    Parameters.Add(Property.Name, Values.ToDataTable(), DbType.Object, ParameterDirection.Input);
                    break;
                }
                case object Value:
                {
                    Parameters.Add(Property.Name, Value);
                    break;
                }
            }
        }

        return Parameters;
    }
}
