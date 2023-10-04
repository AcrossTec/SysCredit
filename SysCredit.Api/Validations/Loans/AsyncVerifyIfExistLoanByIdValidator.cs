namespace SysCredit.Api.Validations.Loans;

using FluentValidation;
using FluentValidation.Validators;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Stores;
using SysCredit.Models;

public class AsyncVerifyIfExistLoanByIdValidator<T> : AsyncPropertyValidator<T, long?>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long? LoanId, CancellationToken Cancellation)
    {
        var Result = await Context.RootContextData[nameof(LoanStore)].AsStore<Loan>().FetchLoanByIdAsync(LoanId);
        return Result is not null;
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return ErrorCodeMessages.GetMessageFromCode(ErrorCode)!;
    }

    public override string Name => "AsyncVerifyIfExistLoanByIdValidator";
}
