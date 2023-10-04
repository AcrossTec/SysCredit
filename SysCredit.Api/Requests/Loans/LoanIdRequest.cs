namespace SysCredit.Api.Requests.Loans;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Loans;

public class LoanIdRequest : IRequest
{
    public long? LoanId { get; set; }
}
