namespace SysCredit.Api.Validations.LoanTypes;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

public class AsyncLoanTypeUniqueNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Name, CancellationToken Cancellation)
    {
        var LoanType = await Context.RootContextData[nameof(LoanTypeStore)].AsStore<LoanType>().FetchLoanTypeByName(Name);
        return LoanType is null;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor";
    }

    public override string Name => "AsyncLoanTypeUniqueNameValidator";
}
