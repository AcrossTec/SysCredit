namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

public class AsyncVerifyLoanTypeReferenceValidator<T> : AsyncPropertyValidator<T, long?>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? LoanTypeId, CancellationToken Cancellation)
    {
        return (await Context.RootContextData[nameof(LoanTypeStore)].AsStore<LoanType>().VerifyLoanTypeReference(LoanTypeId)) is false;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "'{PropertyName}' esta siendo usado en otros registros";
    }

    public override string Name => "AsyncVerifyLoanTypeReferenceValidator";
}
