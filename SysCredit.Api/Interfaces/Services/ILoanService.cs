namespace SysCredit.Api.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Requests.Loans;
using SysCredit.DataTransferObject.Commons;

public interface ILoanService
{
    ValueTask<PaymentPlanInfo?> FetchPaymentPlanFromLoanByLoanIdAsync(LoanIdRequest Request);
}