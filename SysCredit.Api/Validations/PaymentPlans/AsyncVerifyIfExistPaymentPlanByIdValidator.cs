namespace SysCredit.Api.Validations.PaymentPlans;

using FluentValidation;
using FluentValidation.Validators;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;
using SysCredit.Models;


/// <summary>
///     Validador asincrónico para verificar si existe un plan de pagos por su ID.
/// </summary>
/// <typeparam name="T">Tipo de objeto para la validación.</typeparam>
public class AsyncVerifyIfExistPaymentPlanByIdValidator<T> : AsyncPropertyValidator<T, long?>
{
    /// <summary>
    ///     Valida de forma asincrónica si un plan de pagos con un ID dado existe.
    /// </summary>
    /// <param name="Context">Contexto de validación.</param>
    /// <param name="PaymentPlanId">ID del plan de pagos a validar.</param>
    /// <param name="Cancellation">Token de cancelación.</param>
    /// <returns>True si el plan de pagos existe, de lo contrario false.</returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? PaymentPlanId, CancellationToken Cancellation)
    {
        var PaymentPlan = await Context.RootContextData[nameof(PaymentPlanStore)].AsStore<PaymentPlan>().FetchPaymentPlanByIdAsync(PaymentPlanId);
        return PaymentPlan is not null;
    }

    /// <summary>
    ///     Obtiene la plantilla de mensaje predeterminada para el error.
    /// </summary>
    /// <param name="ErrorCode">DATAPP0501.</param>
    /// <returns>Plantilla de mensaje predeterminada correspondiente al código de error.</returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    /// <summary>
    /// Nombre del validador: AsyncVerifyIfExistPaymentPlanByIdValidator.
    /// </summary>
    public override string Name => "AsyncVerifyIfExistPaymentPlanByIdValidator";
}

