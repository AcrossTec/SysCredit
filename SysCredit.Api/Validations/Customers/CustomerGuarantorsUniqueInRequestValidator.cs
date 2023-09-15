namespace SysCredit.Api.Validations.Guarantors;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Requests.Customers;

public class CustomerGuarantorsUniqueInRequestValidator<T> : PropertyValidator<T, IEnumerable<CustomerGuarantorRequest>>
{
    public override bool IsValid(ValidationContext<T> Context, IEnumerable<CustomerGuarantorRequest> GuarantorRequests)
    {
        var Values = GuarantorRequests.DistinctBy(Request => Request.GuarantorId);
        return GuarantorRequests.Count() == Values.Count();
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Request con registros duplicados: 'GuarantorId' debe ser único.";
    }

    public override string Name => "CustomerGuarantorsUniqueInRequestValidator";
}
