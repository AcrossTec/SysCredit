namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using FluentValidation.Validators;
using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

public class AsyncVerifyifCustomerByGuarantorIdValidator <T> : AsyncPropertyValidator<T ,long?>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? GuarantorId, CancellationToken cancellation)
    {
        var Customer = await Context.RootContextData[nameof(CustomerStore)].AsStore<Customer>().FetchCustomerByGuarantorIdAsync(GuarantorId);
        return Customer is null;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return base.GetDefaultMessageTemplate(errorCode);
    }

    public override string Name => " AsyncVerifyifCustomerByGuarantorIdValidator ";
}
