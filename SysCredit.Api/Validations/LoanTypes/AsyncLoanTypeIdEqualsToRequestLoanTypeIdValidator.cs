using FluentValidation;
using FluentValidation.Validators;

namespace SysCredit.Api.Validations.LoanTypes;

public class AsyncLoanTypeIdEqualsToRequestLoanTypeIdValidator<T> : AsyncPropertyValidator<T, long>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long LoanTypeId, CancellationToken Cancellation)
    {
        // Get the LoanTypeId value from the context (LoanTypeId should be passed in the context)
        if (!Context.RootContextData.TryGetValue("LoanTypeId", out var loanTypeIdFromContext))
        {
            return false; // LoanTypeId is not available in the context, validation fails
        }

        var expectedLoanTypeId = (long)loanTypeIdFromContext;

        // Perform your validation logic here
        return LoanTypeId == expectedLoanTypeId;
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "'{PropertyName}' El identificador del tipo de prestamo no coincide";
    }

    public override string Name => "AsyncLoanTypeIdEqualsToRequestLoanTypeIdValidator";
}