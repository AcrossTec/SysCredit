namespace SysCredit.Api.Validations.Guarantors;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Requests.Customers;

/// <summary>
///      Clase Validadora del Request para saber si el Cliente y Fiador es unico.
/// </summary>
/// <typeparam name="T">Tipo del Request que se Va a validar</typeparam>
public class CustomerGuarantorsUniqueInRequestValidator<T> : PropertyValidator<T, IEnumerable<CustomerGuarantorRequest>>
{
    /// <summary>
    ///     Valida que cada solicitud de garantía de cliente sea única por el identificador del fiador.
    /// </summary>
    /// <param name="Context">Obtiene el Objeto donde fue usado el Validador.
    /// </param>
    /// <param name="GuarantorRequests">Una peticion de Fiadores por Clientes.
    /// </param>
    /// <returns>Retorna True si todas las solicitudes son únicas por el identificador del fiador, de lo contrario, False.</returns>
    public override bool IsValid(ValidationContext<T> Context, IEnumerable<CustomerGuarantorRequest> GuarantorRequests)
    {
        var Values = GuarantorRequests.DistinctBy(Request => Request.GuarantorId);
        return GuarantorRequests.Count() == Values.Count();
    }

    /// <summary>
    ///     Nombre de la Validador.
    /// </summary>
    public override string Name => "CustomerGuarantorsUniqueInRequestValidator";
}
