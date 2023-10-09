namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Loan;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;

[Service<ILoanService>]
[ErrorCategory(nameof(LoanService))]
[ErrorCodePrefix(LoanServicePrefix)]
public class LoanService(IStore<Loan> LoanStore, ILogger<LoanService> Logger) : ILoanService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    
    public ValueTask<PaymentPlanInfo?> FetchFromLoanThePaymentPlanByIdAndLoanId(LoandIdWithPaymentPlanIdRequest Request)
    {
        return LoanStore.FetchFromLoanThePaymentPlanByIdAndLoanId(Request);
    }

    public ValueTask<PaymentPlanDetailsInfo?> FetchFromLoanThePaymentPlanDetailsByPaymentPlanIdAndLoanId(LoandIdWithPaymentPlanIdRequest Request)
    {
        return LoanStore.FetchFromLoanThePaymentPlanDetailsByPaymentPlanIdAndLoanId(Request);
    }
}