namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation.Validators;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;

/// <summary>
///     Validador que verifica el Request con el Router
/// </summary>
/// <typeparam name="T">Tipo del Request</typeparam>
public class VerifyRouteWithLoanTypeIdValidator<T> : PropertyValidator<T, long?>
{
    /// <summary>
    ///     Verifica que el LoanTypeId del Request coincida con el Id del Router
    /// </summary>
    /// <param name="Context">Contiene información del Request que se está validando y datos compartidos</param>
    /// <param name="LoanTypeId">Id del LoanType a validar</param>
    /// <returns>Retorna un booleano</returns>
    public override bool IsValid(FluentValidation.ValidationContext<T> Context, long? LoanTypeId)
    {
        // Obtiene el LoanTypeId del Router
        var RouteValue = Context.RootContextData["RouteLoanTypeId"].As<long?>();

        return RouteValue == LoanTypeId;
    }

    /// <summary>
    ///     En caso de error lanza el mensaje con el codigo de error
    /// </summary>
    /// <param name="ErrorCode">Codigo de error asignado al validador</param>
    /// <returns>Retorna un mensaje de error</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    /// <summary>
    ///     Nombre del validador
    /// </summary>
    public override string Name => "VerifyRouteWithLoanTypeIdValidator";
}
