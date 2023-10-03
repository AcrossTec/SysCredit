using SysCredit.Api.Requests.PaymentPlans;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

namespace SysCredit.Api.Interfaces.Services;

public interface IPaymentPlanService
{
    IAsyncEnumerable<PaymentPlanDetailsInfo> FetchPaymentPlanDetailsByPaymentPlanIdAsync(PaymentPlanIdRequest Request);
}
