namespace SysCredit.Api.Validations.Loans;

using FluentValidation;
using FluentValidation.Validators;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;
using SysCredit.Models;

/// <summary>
///     Validador que verifica si un Prestamo Tiene Frequencia de Pago.
/// </summary>
/// <typeparam name="T">
///     Tipo de Request que se va a Validar.
/// </typeparam>
public class AsyncVerifyifExistLoanByPaymentFrequencyIdValidator<T> : AsyncPropertyValidator<T, long?>
{
    /// <summary>
    ///     Valida si el Prestamo Tiene uan Frequencia de pago.
    /// </summary>
    /// <param name="Context">
    ///     Contiene información del Request que se Esta validando
    /// </param>
    /// <param name="PaymentFrequencyId">
    ///     Id del Cliente que se Valida.
    /// </param>
    /// <param name="Cancellation">
    ///     Método que detiene la Validacion.
    /// </param>
    /// <returns>
    ///     Retorna true la frecuencia de pago no está siendo utilizada.
    /// </returns>
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? PaymentFrequencyId, CancellationToken Cancellation)
    {
        var Loan = await Context.RootContextData[nameof(LoanStore)].AsStore<Loan>().FetchLoanByPaymentFrequencyIdAsync(PaymentFrequencyId);
        return Loan is null;
    }

    /// <summary>
    ///     Lanza el error del validador.
    /// </summary>
    /// <param name="ErrorCode">
    ///     Codigo de Error asignado.
    /// </param>
    /// <returns>
    ///     Retorna el mensaje de Error aignado.
    /// </returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    /// <summary>
    ///     Nombre único del Validador.
    /// </summary>
    public override string Name => "AsyncVerifyifExistLoanByPaymentFrequencyIdValidator";
}