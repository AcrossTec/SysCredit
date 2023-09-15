namespace SysCredit.Api.Requests.LoanTypes;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.LoanTypes;

[Validator<DeleteLoanTypeValidator>]
public class DeleteLoanTypeRequest : IRequest
{
    public long? LoanTypeId { get; set; }
}
