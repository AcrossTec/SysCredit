namespace SysCredit.Api.Validations.Guarantors;

using FluentValidation.Validators;
using FluentValidation;
using SysCredit.Api.Stores;
using SysCredit.Api.Extensions;
using SysCredit.Api.Models;
using System.Threading.Tasks;
using System.Threading;

public class AsyncExistsGuarantorValidator<T> : AsyncPropertyValidator<T, long>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long GuarantorId, CancellationToken Cancellation)
    {
        return await Context.RootContextData[nameof(GuarantorStore)].AsStore<Guarantor>().ExistsGuarantorAsync(GuarantorId);
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' no está registrado.";
    }

    public override string Name => "AsyncExistsGuarantorValidator";
}
